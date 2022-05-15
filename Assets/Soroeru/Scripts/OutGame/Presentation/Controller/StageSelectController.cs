using System.Threading;
using Cysharp.Threading.Tasks;
using EFUK;
using Soroeru.Common;
using Soroeru.Common.Domain.UseCase;
using Soroeru.Common.Presentation.Controller;
using Soroeru.OutGame.Presentation.View;
using UniRx;
using UnityEngine;
using VContainer;

namespace Soroeru.OutGame.Presentation.Controller
{
    public sealed class StageSelectController : BaseScreen
    {
        [SerializeField] private RectTransform cursor = default;
        [SerializeField] private StageSelectItemView[] items = default;
        private ReactiveProperty<int> _index;

        public override ScreenType type => ScreenType.Stage;
        private int index => _index.Value;

        private IInputUseCase _inputUseCase;
        private SeController _seController;
        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(IInputUseCase inputUseCase, SeController seController, SceneLoader sceneLoader)
        {
            _inputUseCase = inputUseCase;
            _seController = seController;
            _sceneLoader = sceneLoader;
        }
        
        public override async UniTask InitAsync(CancellationToken token)
        {
            _index = new ReactiveProperty<int>(0);
            _index
                .Subscribe(x =>
                {
                    cursor.transform.localPosition = items[x].localPosition;
                })
                .AddTo(this);

            await UniTask.Yield(token);
        }

        public override async UniTask<ScreenType> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (_inputUseCase.isBack)
                {
                    _seController.Play(SeType.Decision);
                    this.Delay(UiConfig.POP_UP_ANIMATION_TIME, () => _index.Value = 0);
                    return ScreenType.Menu;
                }

                if (_inputUseCase.isDecision)
                {
                    _seController.Play(SeType.Decision);
                    _sceneLoader.LoadFade(items[index].scene);
                    return ScreenType.None;
                }

                var vertical = _inputUseCase.verticalDown;
                if (vertical > 0)
                {
                    _seController.Play(SeType.MoveCursor);   
                    _index.Value = MathfExtension.RepeatDecrement(index, 0, items.GetLastIndex());
                }
                else if (vertical < 0)
                {
                    _seController.Play(SeType.MoveCursor);
                    _index.Value = MathfExtension.RepeatIncrement(index, 0, items.GetLastIndex());
                }

                await UniTask.Yield(token);
            }
        }
    }
}