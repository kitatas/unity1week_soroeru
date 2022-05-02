using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Soroeru.Common.Presentation.View
{
    public sealed class TransitionMaskView : MonoBehaviour
    {
        public async UniTask FadeInAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public async UniTask FadeOutAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }
    }
}