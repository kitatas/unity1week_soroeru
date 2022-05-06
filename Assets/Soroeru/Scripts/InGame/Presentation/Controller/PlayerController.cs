using System;
using EFUK;
using Soroeru.InGame.Domain.UseCase;
using Soroeru.InGame.Presentation.View;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using VContainer.Unity;

namespace Soroeru.InGame.Presentation.Controller
{
    public sealed class PlayerController : IInitializable
    {
        private readonly BuffUseCase _buffUseCase;
        private readonly CoinCountUseCase _coinCountUseCase;
        private readonly CoinUseCase _coinUseCase;
        private readonly IPlayerInputUseCase _inputUseCase;
        private readonly PlayerAnimatorUseCase _animatorUseCase;
        private readonly PlayerAttackUseCase _attackUseCase;
        private readonly PlayerEquipUseCase _equipUseCase;
        private readonly PlayerMoveUseCase _moveUseCase;
        private readonly PlayerRayUseCase _rayUseCase;
        private readonly PlayerSpriteUseCase _spriteUseCase;
        private readonly SlotItemUseCase _slotItemUseCase;
        private readonly PlayerView _playerView;
        private readonly SlotView _slotView;

        public PlayerController(BuffUseCase buffUseCase, CoinCountUseCase coinCountUseCase, CoinUseCase coinUseCase,
            IPlayerInputUseCase inputUseCase, PlayerAnimatorUseCase animatorUseCase,
            PlayerAttackUseCase attackUseCase, PlayerEquipUseCase equipUseCase,
            PlayerMoveUseCase moveUseCase, PlayerRayUseCase rayUseCase, PlayerSpriteUseCase spriteUseCase,
            SlotItemUseCase slotItemUseCase,
            PlayerView playerView, SlotView slotView)
        {
            _buffUseCase = buffUseCase;
            _coinCountUseCase = coinCountUseCase;
            _coinUseCase = coinUseCase;
            _inputUseCase = inputUseCase;
            _animatorUseCase = animatorUseCase;
            _attackUseCase = attackUseCase;
            _equipUseCase = equipUseCase;
            _moveUseCase = moveUseCase;
            _rayUseCase = rayUseCase;
            _spriteUseCase = spriteUseCase;
            _slotItemUseCase = slotItemUseCase;
            _playerView = playerView;
            _slotView = slotView;
        }

        public void Initialize()
        {
            _slotView.Init();

            // 横入力時の制御
            var horizontal = new ReactiveProperty<float>(0.0f);
            horizontal
                .Subscribe(x =>
                {
                    _animatorUseCase.SetRun(x);
                    _spriteUseCase.Flip(x);
                })
                .AddTo(_playerView);

            horizontal
                .Where(x => x.EqualZero() == false)
                .Subscribe(x =>
                {
                    var direction = x > 0 ? Direction.Right : Direction.Left;
                    _playerView.SetDirection(direction);
                })
                .AddTo(_playerView);

            // ジャンプ制御
            var isJump = new ReactiveProperty<bool>(false);
            var canJump = isJump
                .Where(x => x)
                .Where(_ => _rayUseCase.IsGround());
            canJump
                .Subscribe(_ => { _moveUseCase.Jump(); })
                .AddTo(_playerView);

            // 攻撃制御
            var isAttack = new ReactiveProperty<bool>(false);
            isAttack
                .Where(x => x)
                .Subscribe(_ =>
                {
                    _animatorUseCase.SetAttack();
                    _attackUseCase.Attack(_equipUseCase.currentEquip, _playerView.direction);
                })
                .AddTo(_playerView);

            // 被ダメージ
            var isKnockBack = false;
            var isDamage = new ReactiveProperty<bool>(false);
            isDamage
                .Where(x => x)
                .Subscribe(_ =>
                {
                    _animatorUseCase.SetDamage();
                    _moveUseCase.KnockBack(_playerView.direction);
                    _playerView.StartCoroutine(_spriteUseCase.Flash(PlayerConfig.INVINCIBLE_TIME));
                    _playerView.SetLayer(LayerConfig.DAMAGED);

                    isKnockBack = true;
                    _playerView.Delay(PlayerConfig.KNOCK_BACK_TIME, () => isKnockBack = false);
                    _playerView.Delay(PlayerConfig.INVINCIBLE_TIME, () =>
                    {
                        isDamage.Value = false;
                        _playerView.SetLayer(LayerConfig.PLAYER);
                    });
                })
                .AddTo(_playerView);

            // 全てのリールが停止した時
            var reelStop = new Subject<Unit>();
            reelStop
                .Where(_ => _slotView.IsReelStopAll())
                .Subscribe(_ =>
                {
                    var type = _slotView.GetRole();
                    Debug.Log($"role: {type}");
                    _equipUseCase.Equip(type);
                    _slotItemUseCase.Generate(type, _playerView.direction);
                    _buffUseCase.SetUp(type);
                })
                .AddTo(_playerView);

            // 装備更新時
            _equipUseCase.equipType
                .Subscribe(x => { _attackUseCase.EquipWeapon(x); })
                .AddTo(_playerView);

            // 1つのリールを停止
            canJump
                .Merge(isAttack)
                .Merge(isDamage)
                .Where(x => x)
                .Where(_ => _slotView.IsReelStopAll() == false)
                .Subscribe(_ =>
                {
                    _slotView.StopReel();
                    reelStop.OnNext(Unit.Default);
                })
                .AddTo(_playerView);

            // ゲームオーバー
            var isDead = new ReactiveProperty<bool>(false);
            isDead
                .Where(x => x)
                .Subscribe(_ =>
                {
                    _animatorUseCase.SetDead();
                    Debug.Log($"game is over!!");
                })
                .AddTo(_playerView);

            void Damage(int damageValue)
            {
                if (isDamage.Value) return;

                if (_coinCountUseCase.count > 0)
                {
                    isDamage.Value = true;
                    var dropCount = Mathf.Min(_coinCountUseCase.count, damageValue);
                    _coinCountUseCase.Drop(dropCount);
                    _coinUseCase.Drop(_playerView.position, dropCount);
                    return;
                }

                isDead.Value = true;
                return;
            }

            var powerUpTime = 0.0f;
            Action cancelPowerUp = null;

            void PowerUp(int lifeTime)
            {
                powerUpTime = lifeTime;
                if (cancelPowerUp != null)
                {
                    return;
                }

                _moveUseCase.SetPowerUp(true);
                cancelPowerUp = _playerView.PowerUp(lifeTime);
                cancelPowerUp += () =>
                {
                    cancelPowerUp = null;
                    _spriteUseCase.SetColor(Color.white);
                    _moveUseCase.SetPowerUp(false);
                };
            }

            // Buff系
            {
                _buffUseCase.Push(BuffType.Skull, Damage);
                _buffUseCase.Push(BuffType.Seven, PowerUp);
            }

            // TODO: メニュー開いている場合は動かさない
            var tickAsObservable = _playerView.UpdateAsObservable()
                .Where(_ => true);

            var fixedTickAsObservable = _playerView.FixedUpdateAsObservable()
                .Where(_ => true);

            var triggerEnterAsObservable = _playerView.OnTriggerEnter2DAsObservable()
                .Where(_ => true);

            var collisionEnterAsObservable = _playerView.OnCollisionEnter2DAsObservable()
                .Where(_ => true);

            tickAsObservable
                .Subscribe(_ =>
                {
                    var deltaTime = Time.deltaTime;
                    _slotView.Tick(_playerView.transform, deltaTime);
                    _equipUseCase.Tick(deltaTime);
                    _moveUseCase.Tick(deltaTime, _inputUseCase.isJumping);

                    if (cancelPowerUp != null)
                    {
                        _spriteUseCase.PlayRainbow();
                        _coinUseCase.Drop(_slotView.GetDropCoinPosition(), 1);
                        powerUpTime -= deltaTime;
                        if (powerUpTime < 0.0f)
                        {
                            cancelPowerUp.Invoke();
                        }
                    }

                    _animatorUseCase.SetFall(_moveUseCase.gravity);
                    _animatorUseCase.SetGround(_rayUseCase.IsGround());

                    // TODO: Debugのため、あとで消す
                    {
                        if (Input.GetKeyDown(KeyCode.G))
                        {
                            _equipUseCase.Equip(EquipType.Gun);
                        }
                        else if (Input.GetKeyDown(KeyCode.T))
                        {
                            _equipUseCase.Equip(EquipType.Trump);
                        }
                        else if (Input.GetKeyDown(KeyCode.J))
                        {
                            _slotItemUseCase.Generate(ItemType.Jump, _playerView.direction);
                        }
                        else if (Input.GetKeyDown(KeyCode.B))
                        {
                            _slotItemUseCase.Generate(ItemType.Bomb, _playerView.direction);
                        }
                        else if (Input.GetKeyDown(KeyCode.K))
                        {
                            _buffUseCase.SetUp(BuffType.Skull);
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha7))
                        {
                            _buffUseCase.SetUp(BuffType.Seven);
                        }
                    }
                })
                .AddTo(_playerView);

            tickAsObservable
                .Where(_ => isKnockBack == false)
                .Where(_ => isDead.Value == false)
                .Subscribe(_ =>
                {
                    horizontal.Value = _inputUseCase.horizontal;
                    isJump.Value = _inputUseCase.isJump;
                    isAttack.Value = _inputUseCase.isAttack;
                })
                .AddTo(_playerView);

            fixedTickAsObservable
                .Where(_ => isKnockBack == false)
                .Where(_ => isDead.Value == false)
                .Subscribe(_ => { _moveUseCase.SetVelocityX(_inputUseCase.horizontal); })
                .AddTo(_playerView);

            triggerEnterAsObservable
                .Subscribe(other =>
                {
                    if (other.TryGetComponent(out CoinView coinView))
                    {
                        coinView.PickUp(_coinCountUseCase.Increase);
                        return;
                    }
                })
                .AddTo(_playerView);

            collisionEnterAsObservable
                .Select(other => other.collider)
                .Merge(triggerEnterAsObservable)
                .Subscribe(other =>
                {
                    if (other.TryGetComponent(out DamageView damageView))
                    {
                        Damage(damageView.power);
                        return;
                    }
                })
                .AddTo(_playerView);
        }

        public void Jump(float jumpPower)
        {
            _moveUseCase.Jump(jumpPower);
        }
    }
}