using Soroeru.InGame.Data.DataStore;

namespace Soroeru.InGame.Domain.Repository
{
    public sealed class BuffRepository
    {
        private readonly BuffTable _buffTable;

        public BuffRepository(BuffTable buffTable)
        {
            _buffTable = buffTable;
        }

        public BuffData FindBuffData(BuffType type)
        {
            return _buffTable.list
                .Find(x => x.type == type);
        }
    }
}