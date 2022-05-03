using Soroeru.InGame.Data.DataStore;

namespace Soroeru.InGame.Domain.Repository
{
    public sealed class EquipRepository
    {
        private readonly EquipTable _equipTable;

        public EquipRepository(EquipTable equipTable)
        {
            _equipTable = equipTable;
        }

        public EquipData FindEquipData(EquipType type)
        {
            return _equipTable.list
                .Find(x => x.type == type);
        }
    }
}