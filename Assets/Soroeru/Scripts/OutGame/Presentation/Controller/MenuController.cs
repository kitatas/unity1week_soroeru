using System.Threading;
using Cysharp.Threading.Tasks;
using EFUK;
using Soroeru.OutGame.Domain.UseCase;
using Soroeru.OutGame.Presentation.View;
using UniRx;
using UnityEngine;
using VContainer;

namespace Soroeru.OutGame.Presentation.Controller
{
    public sealed class MenuController : BaseScreen
    {
        [SerializeField] private RectTransform cursor = default;
        [SerializeField] private MenuItemView[] items = default;
        private ReactiveProperty<int> _index;

        public override ScreenType type => ScreenType.Menu;
        private int index => _index.Value;
        
        private IInputUseCase _inputUseCase;

        [Inject]
        private void Construct(IInputUseCase inputUseCase)
        {
            _inputUseCase = inputUseCase;
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
                if (_inputUseCase.isDecision)
                {
                    return items[index].type.ConvertScreenType();
                }

                var vertical = _inputUseCase.verticalDown;
                if (vertical > 0)
                {
                    _index.Value = MathfExtension.RepeatDecrement(index, 0, items.GetLastIndex());

                }
                else if (vertical < 0)
                {
                    _index.Value = MathfExtension.RepeatIncrement(index, 0, items.GetLastIndex());
                }

                await UniTask.Yield(token);
            }
        }
    }
}