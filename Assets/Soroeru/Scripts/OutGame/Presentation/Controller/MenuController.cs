using System.Threading;
using Cysharp.Threading.Tasks;
using Soroeru.Common;
using Soroeru.Common.Domain.UseCase;
using Soroeru.Common.Presentation.Controller;
using Soroeru.OutGame.Presentation.View;
using UniRx;

namespace Soroeru.OutGame.Presentation.Controller
{
    public sealed class MenuController : BaseScreen
    {
        public override ScreenType type => ScreenType.Menu;

        private readonly IInputUseCase _inputUseCase;
        private readonly ItemIndexUseCase _indexUseCase;
        private readonly SeController _seController;
        private readonly MenuView _menuView;

        public MenuController(IInputUseCase inputUseCase, ItemIndexUseCase indexUseCase, SeController seController,
            MenuView menuView)
        {
            _inputUseCase = inputUseCase;
            _indexUseCase = indexUseCase;
            _seController = seController;
            _menuView = menuView;
        }

        public override async UniTask InitAsync(CancellationToken token)
        {
            _indexUseCase.index
                .Subscribe(_menuView.SetCursorPosition)
                .AddTo(_menuView);

            await UniTask.Yield(token);
        }

        public override async UniTask<ScreenType> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (_inputUseCase.isDecision)
                {
                    _seController.Play(SeType.Decision);
                    return _menuView.GetCurrentType(_indexUseCase.value);
                }

                var vertical = _inputUseCase.verticalDown;
                if (vertical > 0)
                {
                    _seController.Play(SeType.MoveCursor);
                    _indexUseCase.RepeatDecrement(_menuView.itemLastIndex);
                }
                else if (vertical < 0)
                {
                    _seController.Play(SeType.MoveCursor);
                    _indexUseCase.RepeatIncrement(_menuView.itemLastIndex);
                }

                await UniTask.Yield(token);
            }
        }
    }
}