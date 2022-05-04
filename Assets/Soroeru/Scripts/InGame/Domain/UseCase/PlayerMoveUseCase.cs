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
            _rigidbody2d.velocity = _moveData.speed * x * Vector2.right + GetGravityVector();
        }

        private Vector2 GetGravityVector()
        {
            return new Vector2(0.0f, gravity);
        }

        public float gravity => _rigidbody2d.velocity.y;

        public void Jump(bool isHigh)
        {
            var jumpPower = isHigh ? _moveData.highJump : _moveData.lowJump;
            Jump(jumpPower);
        }

        public void Jump(float jumpPower)
        {
            _rigidbody2d.AddForce(jumpPower * Vector2.up);
        }

        public void KnockBack(Direction direction)
        {
            var backVector = direction.ConvertVector2() * -1;
            var knockVector =
                backVector * PlayerConfig.KNOCK_BACK_POWER +
                Vector2.up * PlayerConfig.KNOCK_UP_POWER;
            _rigidbody2d.AddForce(knockVector);
        }
    }
}