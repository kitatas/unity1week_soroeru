using Soroeru.InGame.Domain.UseCase;
using Soroeru.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Soroeru.InGame.Presentation.Presenter
{
    public sealed class CoinCountPresenter : IInitializable
    {
        private readonly CoinCountUseCase _coinCountUseCase;
        private readonly CoinCountView _coinCountView;

        public CoinCountPresenter(CoinCountUseCase coinCountUseCase, CoinCountView coinCountView)
        {
            _coinCountUseCase = coinCountUseCase;
            _coinCountView = coinCountView;
        }

        public void Initialize()
        {
            _coinCountUseCase.coinCount
                .Subscribe(_coinCountView.Display)
                .AddTo(_coinCountView);
        }
    }
}