using Soroeru.InGame.Data.Entity;
using UniRx;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class CoinCountUseCase
    {
        private readonly CoinCountEntity _coinCountEntity;
        private readonly ReactiveProperty<int> _coinCount;

        public CoinCountUseCase(CoinCountEntity coinCountEntity)
        {
            _coinCountEntity = coinCountEntity;
            _coinCount = new ReactiveProperty<int>(_coinCountEntity.value);
        }

        public IReadOnlyReactiveProperty<int> coinCount => _coinCount;

        public void Increase(int value)
        {
            _coinCountEntity.Add(value);
            _coinCount.Value = _coinCountEntity.value;
        }

        public void Drop()
        {
            _coinCountEntity.Set(0);
            _coinCount.Value = _coinCountEntity.value;
        }
    }
}