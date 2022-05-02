using Soroeru.Common.Data.DataStore;

namespace Soroeru.Common.Domain.Repository
{
    public sealed class SoundRepository
    {
        private readonly BgmTable _bgmTable;

        public SoundRepository(BgmTable bgmTable)
        {
            _bgmTable = bgmTable;
        }

        public BgmData FindBgm(BgmType type)
        {
            return _bgmTable.list
                .Find(x => x.type == type);
        }
    }
}