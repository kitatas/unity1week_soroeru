using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Soroeru.OutGame.Presentation.Controller
{
    public abstract class BaseScreen : MonoBehaviour
    {
        public abstract ScreenType type { get; }

#pragma warning disable CS1998
        public virtual async UniTask InitAsync(CancellationToken token)
#pragma warning restore CS1998
        {
        }

#pragma warning disable CS1998
        public virtual async UniTask<ScreenType> TickAsync(CancellationToken token)
#pragma warning restore CS1998
        {
            return ScreenType.None;
        }
    }
}