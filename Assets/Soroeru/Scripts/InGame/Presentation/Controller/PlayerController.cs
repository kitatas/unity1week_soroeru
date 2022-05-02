using Soroeru.InGame.Domain.UseCase;
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
        private PlayerMoveUseCase _moveUseCase;
        private PlayerRayUseCase _rayUseCase;
        private PlayerSpriteUseCase _spriteUseCase;

        [Inject]
        private void Construct(IPlayerInputUseCase inputUseCase, PlayerMoveUseCase moveUseCase,
            PlayerRayUseCase rayUseCase, PlayerSpriteUseCase spriteUseCase)
        {
            _inputUseCase = inputUseCase;
            _moveUseCase = moveUseCase;
            _rayUseCase = rayUseCase;
            _spriteUseCase = spriteUseCase;
        }

        private void Start()
        {
            // 横入力時の制御
            var horizontal = new ReactiveProperty<float>(0.0f);
            horizontal
                .Subscribe(x =>
                {
                    // TODO: アニメーション制御
                    _moveUseCase.SetVelocityX(x);
                    _spriteUseCase.Flip(x);
                })
                .AddTo(this);

            // ジャンプ制御
            var isJump = new ReactiveProperty<bool>(false);
            isJump
                .Where(_ => _rayUseCase.IsGround())
                .Subscribe(_ =>
                {
                    // TODO: 長押し判定はここで行う？
                    var isLongDown = _;
                    _moveUseCase.Jump(isLongDown);
                })
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
                })
                .AddTo(this);
        }
    }
}