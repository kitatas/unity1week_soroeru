using Soroeru.InGame.Data.DataStore;

namespace Soroeru.InGame.Domain.Repository
{
    public sealed class AttackRepository
    {
        private readonly AttackTable _attackTable;

        public AttackRepository(AttackTable attackTable)
        {
            _attackTable = attackTable;
        }

        public AttackData FindAttackData(EquipType type)
        {
            return _attackTable.list.Find(x => x.type == type);
        }
    }
}