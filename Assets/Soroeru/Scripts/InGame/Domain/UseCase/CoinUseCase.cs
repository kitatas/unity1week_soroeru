using Soroeru.InGame.Domain.Factory;
using Soroeru.InGame.Domain.Repository;
using UnityEngine;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class CoinUseCase
    {
        private readonly CoinFactory _coinFactory;
        private readonly ItemRepository _itemRepository;

        public CoinUseCase(CoinFactory coinFactory, ItemRepository itemRepository)
        {
            _coinFactory = coinFactory;
            _itemRepository = itemRepository;
        }

        public void Drop(Vector3 position, int value)
        {
            for (int i = 0; i < value; i++)
            {
                var coin = _coinFactory.Generate(_itemRepository.GetCoin(), position);
                coin.Drop();
            }
        }
    }
}