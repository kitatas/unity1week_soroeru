using System.Threading;
using Cysharp.Threading.Tasks;
using Soroeru.Common;
using Soroeru.Common.Domain.UseCase;
using Soroeru.Common.Presentation.Controller;
using Soroeru.OutGame.Presentation.View;

namespace Soroeru.OutGame.Presentation.Controller
{
    public sealed class TopController : BaseScreen
    {
        public override ScreenType type => ScreenType.Top;

        private readonly IInputUseCase _inputUseCase;
        private readonly SeController _seController;
        private readonly TopView _topView;

        public TopController(IInputUseCase inputUseCase, BgmController bgmController, SeController seController,
            TopView topView)
        {
            _inputUseCase = inputUseCase;
            _seController = seController;
            _topView = topView;

            bgmController.Play(BgmType.Top);
        }

        public override async UniTask InitAsync(CancellationToken token)
        {
            _topView.FlashPress();

            await UniTask.Yield(token);
        }

        public override async UniTask<ScreenType> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (_inputUseCase.isAnyKey)
                {
                    _seController.Play(SeType.Decision);
                    return ScreenType.Menu;
                }

                await UniTask.Yield(token);
            }
        }
    }
}