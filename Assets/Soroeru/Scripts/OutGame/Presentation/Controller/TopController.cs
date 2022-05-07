using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Soroeru.OutGame.Domain.UseCase;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Soroeru.OutGame.Presentation.Controller
{
    public sealed class TopController : BaseScreen
    {
        [SerializeField] private Graphic press = default;

        public override ScreenType type => ScreenType.Top;

        private IInputUseCase _inputUseCase;

        [Inject]
        private void Construct(IInputUseCase inputUseCase)
        {
            _inputUseCase = inputUseCase;
        }

        public override async UniTask InitAsync(CancellationToken token)
        {
            press
                .DOFade(0.0f, 0.2f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(press.gameObject);

            await UniTask.Yield(token);
        }

        public override async UniTask<ScreenType> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (_inputUseCase.isAnyKey)
                {
                    return ScreenType.Menu;
                }

                await UniTask.Yield(token);
            }
        }
    }
}