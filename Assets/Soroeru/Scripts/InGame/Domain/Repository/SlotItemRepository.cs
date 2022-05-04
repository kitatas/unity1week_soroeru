using Soroeru.InGame.Data.DataStore;

namespace Soroeru.InGame.Domain.Repository
{
    public sealed class SlotItemRepository
    {
        private readonly SlotItemTable _slotItemTable;

        public SlotItemRepository(SlotItemTable slotItemTable)
        {
            _slotItemTable = slotItemTable;
        }

        public SlotItemData FindSlotItemData(ItemType type)
        {
            return _slotItemTable.list
                .Find(x => x.type == type);
        }
    }
}