using System.Threading;
using Cysharp.Threading.Tasks;
using Soroeru.Common;
using Soroeru.Common.Domain.UseCase;
using Soroeru.Common.Presentation.Controller;
using Soroeru.OutGame.Domain.UseCase;
using Soroeru.OutGame.Presentation.View;
using UniRx;

namespace Soroeru.OutGame.Presentation.Controller
{
    public sealed class StageSelectController : BaseScreen
    {
        public override ScreenType type => ScreenType.Stage;

        private readonly IInputUseCase _inputUseCase;
        private readonly ItemIndexUseCase _indexUseCase;
        private readonly SeController _seController;
        private readonly SceneLoader _sceneLoader;
        private readonly StageSelectView _stageSelectView;

        public StageSelectController(IInputUseCase inputUseCase, ItemIndexUseCase indexUseCase,
            SeController seController, SceneLoader sceneLoader, StageSelectView stageSelectView)
        {
            _inputUseCase = inputUseCase;
            _indexUseCase = indexUseCase;
            _seController = seController;
            _sceneLoader = sceneLoader;
            _stageSelectView = stageSelectView;
        }

        public override async UniTask InitAsync(CancellationToken token)
        {
            _indexUseCase.index
                .Subscribe(_stageSelectView.SetCursorPosition)
                .AddTo(_stageSelectView);

            await UniTask.Yield(token);
        }

        public override async UniTask<ScreenType> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (_inputUseCase.isBack)
                {
                    _seController.Play(SeType.Decision);
                    _stageSelectView.ResetView(() => _indexUseCase.ResetValue());
                    return ScreenType.Menu;
                }

                if (_inputUseCase.isDecision)
                {
                    _seController.Play(SeType.Decision);
                    _sceneLoader.LoadFade(_stageSelectView.GetSceneName(_indexUseCase.value));
                    return ScreenType.None;
                }

                var vertical = _inputUseCase.verticalDown;
                if (vertical > 0)
                {
                    _seController.Play(SeType.MoveCursor);
                    _indexUseCase.RepeatDecrement(_stageSelectView.itemLastIndex);
                }
                else if (vertical < 0)
                {
                    _seController.Play(SeType.MoveCursor);
                    _indexUseCase.RepeatIncrement(_stageSelectView.itemLastIndex);
                }

                await UniTask.Yield(token);
            }
        }
    }
}