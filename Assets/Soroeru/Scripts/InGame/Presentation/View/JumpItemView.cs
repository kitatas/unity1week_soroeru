using DG.Tweening;
using Soroeru.Common;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class JumpItemView : BaseSlotItem
    {
        [SerializeField] private float jumpPower = default;
        [SerializeField] private ContactFilter2D filter2d = default;

        protected override void Start()
        {
            base.Start();

            var rigidbody2d = GetComponent<Rigidbody2D>();
            this.OnCollisionEnter2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.collider.TryGetComponent(out PlayerView player))
                    {
                        // 上部判定
                        if (rigidbody2d.IsTouching(filter2d))
                        {
                            seController.Play(SeType.Jump);
                            player.jumpAction?.Invoke(jumpPower);
                            PlayAnimation();
                        }
                    }
                })
                .AddTo(this);
        }

        private void PlayAnimation()
        {
            var defaultScale = 0.2f;
            var animationTime = 0.1f;
            DOTween.Sequence()
                .Append(transform
                    .DOScaleX(defaultScale + 0.1f, animationTime))
                .Join(transform
                    .DOScaleY(defaultScale - 0.1f, animationTime))
                .Append(transform
                    .DOScaleX(defaultScale - 0.1f, animationTime))
                .Join(transform
                    .DOScaleY(defaultScale + 0.1f, animationTime))
                .Append(transform
                    .DOScaleX(defaultScale, animationTime))
                .Join(transform
                    .DOScaleY(defaultScale, animationTime))
                .SetLink(gameObject);
        }
    }
}