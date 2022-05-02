using Soroeru.Common.Data.DataStore;

namespace Soroeru.Common.Domain.Repository
{
    public sealed class SoundRepository
    {
        private readonly BgmTable _bgmTable;
        private readonly SeTable _seTable;

        public SoundRepository(BgmTable bgmTable, SeTable seTable)
        {
            _bgmTable = bgmTable;
            _seTable = seTable;
        }

        public BgmData FindBgm(BgmType type)
        {
            return _bgmTable.list
                .Find(x => x.type == type);
        }

        public SeData FindSe(SeType type)
        {
            return _seTable.list
                .Find(x => x.type == type);
        }
    }
}