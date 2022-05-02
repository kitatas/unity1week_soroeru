using Soroeru.InGame.Data.DataStore;
using Soroeru.InGame.Domain.Repository;
using UnityEngine;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class PlayerMoveUseCase
    {
        private readonly Rigidbody2D _rigidbody2d;
        private readonly PlayerMoveData _moveData;

        public PlayerMoveUseCase(Rigidbody2D rigidbody2D, PlayerRepository playerRepository)
        {
            _rigidbody2d = rigidbody2D;
            _moveData = playerRepository.GetMoveData();
        }

        public void SetVelocityX(float x)
        {
            _rigidbody2d.velocity = _moveData.speed * x * Vector2.right;
        }

        public void Jump(bool isHigh)
        {
            var jumpPower = isHigh ? _moveData.highJump : _moveData.lowJump;
            _rigidbody2d.AddForce(jumpPower * Vector2.up);
        }
    }
}