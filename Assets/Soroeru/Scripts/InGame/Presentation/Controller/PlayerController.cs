using System;
using EFUK;
using Soroeru.Common;
using Soroeru.Common.Domain.UseCase;
using Soroeru.Common.Presentation.Controller;
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
        private readonly IInputUseCase _inputUseCase;
        private readonly PlayerAnimatorUseCase _animatorUseCase;
        private readonly PlayerAttackUseCase _attackUseCase;
        private readonly PlayerDirectionUseCase _directionUseCase;
        private readonly PlayerEquipUseCase _equipUseCase;
        private readonly PlayerMoveUseCase _moveUseCase;
        private readonly PlayerRayUseCase _rayUseCase;
        private readonly PlayerSpriteUseCase _spriteUseCase;
        private readonly SlotItemUseCase _slotItemUseCase;
        private readonly TweetUseCase _tweetUseCase;
        private readonly TimeUseCase _timeUseCase;
        private readonly PlayerView _playerView;
        private readonly SlotView _slotView;
        private readonly BgmController _bgmController;
        private readonly SeController _seController;
        private readonly SceneLoader _sceneLoader;

        public PlayerController(BuffUseCase buffUseCase, CoinCountUseCase coinCountUseCase, CoinUseCase coinUseCase,
            IInputUseCase inputUseCase, PlayerAnimatorUseCase animatorUseCase,
            PlayerAttackUseCase attackUseCase, PlayerDirectionUseCase directionUseCase, PlayerEquipUseCase equipUseCase,
            PlayerMoveUseCase moveUseCase, PlayerRayUseCase rayUseCase, PlayerSpriteUseCase spriteUseCase,
            SlotItemUseCase slotItemUseCase, TweetUseCase tweetUseCase, TimeUseCase timeUseCase,
            PlayerView playerView, SlotView slotView,
            BgmController bgmController, SeController seController, SceneLoader sceneLoader)
        {
            _buffUseCase = buffUseCase;
            _coinCountUseCase = coinCountUseCase;
            _coinUseCase = coinUseCase;
            _inputUseCase = inputUseCase;
            _animatorUseCase = animatorUseCase;
            _attackUseCase = attackUseCase;
            _directionUseCase = directionUseCase;
            _equipUseCase = equipUseCase;
            _moveUseCase = moveUseCase;
            _rayUseCase = rayUseCase;
            _spriteUseCase = spriteUseCase;
            _slotItemUseCase = slotItemUseCase;
            _tweetUseCase = tweetUseCase;
            _timeUseCase = timeUseCase;
            _playerView = playerView;
            _slotView = slotView;
            _bgmController = bgmController;
            _seController = seController;
            _sceneLoader = sceneLoader;
        }

        public void Initialize()
        {
            _bgmController.Play(BgmType.Main);
            _seController.PlayLoop(SeType.ReelRoll, true);
            _slotView.Init();
            _playerView.Init(_moveUseCase.Jump);

            // ?????????????????????
            var horizontal = new ReactiveProperty<float>(0.0f);
            horizontal
                .Subscribe(x =>
                {
                    _directionUseCase.Set(x);
                    _animatorUseCase.SetRun(x);
                    _spriteUseCase.Flip(x);
                })
                .AddTo(_playerView);

            // ??????????????????
            var isJump = new ReactiveProperty<bool>(false);
            var canJump = isJump
                .Where(x => x)
                .Where(_ => _rayUseCase.IsGround());
            canJump
                .Subscribe(_ => { _moveUseCase.Jump(); })
                .AddTo(_playerView);

            // ????????????
            var isAttack = new ReactiveProperty<bool>(false);
            isAttack
                .Where(x => x)
                .Subscribe(_ =>
                {
                    if (_equipUseCase.currentEquip == EquipType.Gun)
                    {
                        _seController.Play(SeType.Gun);
                    }
                    _animatorUseCase.SetAttack();
                    _attackUseCase.Attack(_equipUseCase.currentEquip, _directionUseCase.current);
                })
                .AddTo(_playerView);

            // ???????????????
            var isKnockBack = false;
            var isDamage = new ReactiveProperty<bool>(false);
            isDamage
                .Where(x => x)
                .Subscribe(_ =>
                {
                    _animatorUseCase.SetDamage();
                    _moveUseCase.KnockBack(_directionUseCase.current);
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

            var isGoal = new ReactiveProperty<bool>(false);

            // ????????????????????????????????????
            var reelStop = new Subject<Unit>();
            reelStop
                .Where(_ => _slotView.IsReelStopAll())
                .Subscribe(_ =>
                {
                    _playerView.Delay(SlotConfig.REEL_ROTATE_INTERVAL, () =>
                    {
                        if (isGoal.Value == false)
                        {
                            _seController.PlayLoop(SeType.ReelRoll);
                        }
                    });
                    _seController.Stop(SeType.ReelRoll);
                    _seController.Play(SeType.ReelStop);

                    var type = _slotView.GetRole();

                    var isEquip = _equipUseCase.Equip(type);
                    var isGenerate = _slotItemUseCase.Generate(type, _directionUseCase.current);
                    var buffType = _buffUseCase.SetUp(type);
                    if (isEquip || isGenerate)
                    {
                        _seController.Play(SeType.HitRoleNormal);
                    }
                    else if (buffType == BuffType.Seven)
                    {
                        _seController.Play(SeType.HitRoleSeven);
                    }
                    else if (buffType == BuffType.Skull)
                    {
                        _seController.Play(SeType.HitRoleSkull);
                    }
                })
                .AddTo(_playerView);

            // ???????????????
            _equipUseCase.equipType
                .Subscribe(x => { _attackUseCase.EquipWeapon(x); })
                .AddTo(_playerView);

            // 1????????????????????????
            canJump
                .Merge(isAttack)
                .Merge(isDamage)
                .Where(x => x)
                .Where(_ => _slotView.IsReelStopAll() == false)
                .Subscribe(_ =>
                {
                    _seController.Play(SeType.ReelStop);
                    _slotView.StopReel();
                    reelStop.OnNext(Unit.Default);
                })
                .AddTo(_playerView);

            // ?????????????????????
            var isDead = new ReactiveProperty<bool>(false);
            isDead
                .Where(x => x)
                .Subscribe(_ =>
                {
                    _animatorUseCase.SetDead();
                    _playerView.Delay(1.0f, _sceneLoader.LoadFadeCurrent);
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

                _seController.PlayLoop(SeType.SevenFever, true);
                _moveUseCase.SetPowerUp(true);
                cancelPowerUp = _playerView.PowerUp(lifeTime);
                cancelPowerUp += () =>
                {
                    cancelPowerUp = null;
                    _spriteUseCase.SetColor(Color.white);
                    _moveUseCase.SetPowerUp(false);
                    if (_slotView.IsReelStopAll())
                    {
                        _seController.Stop(SeType.SevenFever);
                    }
                    else
                    {
                        _seController.PlayLoop(SeType.ReelRoll, true);
                    }
                };
            }

            // Buff???
            {
                _buffUseCase.Push(BuffType.Skull, Damage);
                _buffUseCase.Push(BuffType.Seven, PowerUp);
            }

            var isStickingLeft = false;
            var isStickingRight = false;

            _playerView.UpdateAsObservable()
                .Where(_ => _inputUseCase.isMenu)
                .Where(_ => isGoal.Value == false)
                .Subscribe(_ =>
                {
                    _seController.Play(SeType.Decision);
                    _timeUseCase.SetPause();
                })
                .AddTo(_playerView);

            var tickAsObservable = _playerView.UpdateAsObservable()
                .Where(_ => isGoal.Value == false && _timeUseCase.isPause == false);

            var fixedTickAsObservable = _playerView.FixedUpdateAsObservable()
                .Where(_ => isGoal.Value == false && _timeUseCase.isPause == false);

            var triggerEnterAsObservable = _playerView.OnTriggerEnter2DAsObservable()
                .Where(_ => isGoal.Value == false && _timeUseCase.isPause == false);

            var collisionEnterAsObservable = _playerView.OnCollisionEnter2DAsObservable()
                .Where(_ => isGoal.Value == false && _timeUseCase.isPause == false);

            var collisionStayAsObservable = _playerView.OnCollisionStay2DAsObservable()
                .Where(_ => isGoal.Value == false && _timeUseCase.isPause == false);

            var collisionExitAsObservable = _playerView.OnCollisionExit2DAsObservable()
                .Where(_ => isGoal.Value == false && _timeUseCase.isPause == false);

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

                    // TODO: Debug???????????????????????????
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
                            _slotItemUseCase.Generate(ItemType.Jump, _directionUseCase.current);
                        }
                        else if (Input.GetKeyDown(KeyCode.B))
                        {
                            _slotItemUseCase.Generate(ItemType.Bomb, _directionUseCase.current);
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
                .Subscribe(_ =>
                {
                    if (_inputUseCase.horizontal < 0 && isStickingLeft ||
                        _inputUseCase.horizontal > 0 && isStickingRight)
                    {
                        return;
                    }
                    _moveUseCase.SetVelocityX(_inputUseCase.horizontal);
                })
                .AddTo(_playerView);

            triggerEnterAsObservable
                .Subscribe(other =>
                {
                    if (other.TryGetComponent(out CoinView coinView))
                    {
                        _seController.Play(SeType.CoinGet);
                        coinView.PickUp(_coinCountUseCase.Increase);
                        return;
                    }

                    if (other.TryGetComponent(out GoalView goalView))
                    {
                        isGoal.Value = true;
                        _seController.Stop();
                        _bgmController.Play(BgmType.Clear);
                        _animatorUseCase.SetClear();
                        _slotView.gameObject.SetActive(false);
                        // _tweetUseCase.Tweet(_coinCountUseCase.count);

                        _playerView.Delay(3.0f, () =>
                        {
                            _animatorUseCase.SetFinish();
                            _playerView.Tick(() => _moveUseCase.SetVelocityX(1));
                        });

                        _playerView.Delay(4.0f, () =>
                        {
                            _sceneLoader.LoadFadeNext();
                        });

                        goalView.DisplayClearText(_sceneLoader.currentScene);
                        goalView.Play(x =>
                        {
                            _coinUseCase.Drop(x, 1);
                        });
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
                        _seController.Play(SeType.CoinDrop);
                        Damage(damageView.power);
                        return;
                    }
                })
                .AddTo(_playerView);

            collisionStayAsObservable
                .Select(other => other.collider)
                .Where(other => other.gameObject.layer == LayerMask.NameToLayer(LayerConfig.GROUND))
                .Subscribe(other =>
                {
                    isStickingLeft = _moveUseCase.HitWall(_playerView.left);
                    isStickingRight = _moveUseCase.HitWall(_playerView.right);
                })
                .AddTo(_playerView);

            collisionExitAsObservable
                .Select(other => other.collider)
                .Where(other => other.gameObject.layer == LayerMask.NameToLayer(LayerConfig.GROUND))
                .Subscribe(other =>
                {
                    isStickingLeft = false;
                    isStickingRight = false;
                })
                .AddTo(_playerView);
        }
    }
}