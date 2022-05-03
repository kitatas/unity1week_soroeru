using EFUK;
using Soroeru.InGame.Domain.UseCase;
using Soroeru.InGame.Presentation.View;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using VContainer;

namespace Soroeru.InGame.Presentation.Controller
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerController : MonoBehaviour
    {
        private IPlayerInputUseCase _inputUseCase;
        private PlayerAnimatorUseCase _animatorUseCase;
        private PlayerAttackUseCase _attackUseCase;
        private PlayerEquipUseCase _equipUseCase;
        private PlayerMoveUseCase _moveUseCase;
        private PlayerRayUseCase _rayUseCase;
        private PlayerSpriteUseCase _spriteUseCase;
        private SlotView _slotView;

        [Inject]
        private void Construct(IPlayerInputUseCase inputUseCase, PlayerAnimatorUseCase animatorUseCase,
            PlayerAttackUseCase attackUseCase, PlayerEquipUseCase equipUseCase,
            PlayerMoveUseCase moveUseCase, PlayerRayUseCase rayUseCase, PlayerSpriteUseCase spriteUseCase,
            SlotView slotView)
        {
            _inputUseCase = inputUseCase;
            _animatorUseCase = animatorUseCase;
            _attackUseCase = attackUseCase;
            _equipUseCase = equipUseCase;
            _moveUseCase = moveUseCase;
            _rayUseCase = rayUseCase;
            _spriteUseCase = spriteUseCase;
            _slotView = slotView;
        }

        private void Start()
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
                .AddTo(this);

            var direction = Direction.Right;
            horizontal
                .Where(x => x.EqualZero() == false)
                .Subscribe(x => direction = x > 0 ? Direction.Right : Direction.Left)
                .AddTo(this);

            // ジャンプ制御
            var isJump = new ReactiveProperty<bool>(false);
            isJump
                .Where(x => x)
                .Where(_ => _rayUseCase.IsGround())
                .Subscribe(_ =>
                {
                    // TODO: 長押し判定はここで行う？
                    var isLongDown = _;
                    _moveUseCase.Jump(isLongDown);
                })
                .AddTo(this);

            // 攻撃制御
            var isAttack = new ReactiveProperty<bool>(false);
            isAttack
                .Where(x => x)
                .Subscribe(_ =>
                {
                    _animatorUseCase.SetAttack();
                    _attackUseCase.Attack(_equipUseCase.currentEquip, direction);
                })
                .AddTo(this);

            // スロット停止
            isJump
                .Merge(isAttack)
                // TODO: ダメージ時
                .Where(x => x)
                .Subscribe(_ => _slotView.StopReel())
                .AddTo(this);

            // TODO: メニュー開いている場合は動かさない
            var tickAsObservable = this.UpdateAsObservable()
                .Where(_ => true);

            var fixedTickAsObservable = this.FixedUpdateAsObservable()
                .Where(_ => true);

            tickAsObservable
                .Subscribe(_ =>
                {
                    horizontal.Value = _inputUseCase.horizontal;
                    isJump.Value = _inputUseCase.isJump;
                    isAttack.Value = _inputUseCase.isAttack;

                    var deltaTime = Time.deltaTime;
                    _slotView.Tick(transform, deltaTime);
                    _equipUseCase.Tick(deltaTime);

                    _animatorUseCase.SetGround(_rayUseCase.IsGround());
                    _animatorUseCase.SetFall(_moveUseCase.gravity);
                })
                .AddTo(this);

            fixedTickAsObservable
                .Subscribe(_ =>
                {
                    _moveUseCase.SetVelocityX(_inputUseCase.horizontal);
                })
                .AddTo(this);
        }
    }
}