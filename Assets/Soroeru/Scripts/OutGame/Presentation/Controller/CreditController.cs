using System.Threading;
using Cysharp.Threading.Tasks;
using EFUK;
using Soroeru.Common;
using Soroeru.Common.Presentation.Controller;
using Soroeru.OutGame.Domain.UseCase;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Soroeru.OutGame.Presentation.Controller
{
    public sealed class CreditController : BaseScreen
    {
        [SerializeField] private ScrollRect scrollRect = default;

        public override ScreenType type => ScreenType.Credit;

        private IInputUseCase _inputUseCase;
        private SeController _seController;

        [Inject]
        private void Construct(IInputUseCase inputUseCase, SeController seController)
        {
            _inputUseCase = inputUseCase;
            _seController = seController;
        }

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<ScreenType> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (_inputUseCase.isBack)
                {
                    _seController.Play(SeType.Decision);
                    this.Delay(UiConfig.POP_UP_ANIMATION_TIME, () => scrollRect.verticalNormalizedPosition = 1.0f);
                    return ScreenType.Menu;
                }

                if (_inputUseCase.vertical != 0.0f)
                {
                    scrollRect.verticalNormalizedPosition += _inputUseCase.vertical * Time.deltaTime * 0.1f;
                }

                await UniTask.Yield(token);
            }
        }
    }
}