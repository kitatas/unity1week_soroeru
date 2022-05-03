using Soroeru.Common.Data.Entity;

namespace Soroeru.InGame.Data.Entity
{
    public sealed class CoinCountEntity : BaseEntity<int>
    {
        public CoinCountEntity()
        {
            Set(0);
        }

        public void Add(int addValue)
        {
            Set(value + addValue);
        }
    }
}