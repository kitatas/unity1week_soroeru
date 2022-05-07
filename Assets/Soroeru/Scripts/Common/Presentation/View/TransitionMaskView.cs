using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Soroeru.Common.Presentation.View
{
    public sealed class TransitionMaskView : MonoBehaviour
    {
        [SerializeField] private Graphic mask = default;

        private readonly float _animationTime = 0.5f;

        public async UniTask FadeInAsync(CancellationToken token)
        {
            await mask
                .DOFade(1.0f, _animationTime)
                .WithCancellation(token);
        }

        public async UniTask FadeOutAsync(CancellationToken token)
        {
            await mask
                .DOFade(0.0f, _animationTime)
                .WithCancellation(token);
        }
    }
}