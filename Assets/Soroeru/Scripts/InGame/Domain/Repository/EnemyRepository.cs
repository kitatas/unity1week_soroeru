using Soroeru.InGame.Data.DataStore;

namespace Soroeru.InGame.Domain.Repository
{
    public sealed class EnemyRepository
    {
        private readonly EnemyTable _enemyTable;

        public EnemyRepository(EnemyTable enemyTable)
        {
            _enemyTable = enemyTable;
        }

        public EnemyData FindEnemyData(EnemyType type)
        {
            return _enemyTable.list
                .Find(x => x.type == type);
        }
    }
}