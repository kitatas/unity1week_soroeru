using Soroeru.InGame.Data.DataStore;
using Soroeru.InGame.Presentation.View;

namespace Soroeru.InGame.Domain.Repository
{
    public sealed class ItemRepository
    {
        private readonly ItemTable _itemTable;

        public ItemRepository(ItemTable itemTable)
        {
            _itemTable = itemTable;
        }

        public CoinView GetCoin()
        {
            return _itemTable.coin;
        }
    }
}