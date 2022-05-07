using System;
using UniRx;

namespace Soroeru.OutGame.Domain.UseCase
{
    public sealed class ScreenUseCase
    {
        private readonly ReactiveProperty<ScreenType> _screenType;

        public ScreenUseCase()
        {
            _screenType = new ReactiveProperty<ScreenType>(ScreenType.Top);
        }

        public IObservable<ScreenType> screenType => _screenType
            .Where(x => x != ScreenType.None);

        public void SetType(ScreenType state)
        {
            _screenType.Value = state;
        }
    }
}