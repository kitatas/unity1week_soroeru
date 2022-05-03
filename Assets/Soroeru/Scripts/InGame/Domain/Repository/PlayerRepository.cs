using Soroeru.InGame.Data.DataStore;

namespace Soroeru.InGame.Domain.Repository
{
    public sealed class PlayerRepository
    {
        private readonly PlayerData _playerData;

        public PlayerRepository(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public PlayerMoveData GetMoveData() => _playerData.moveData;
    }
}