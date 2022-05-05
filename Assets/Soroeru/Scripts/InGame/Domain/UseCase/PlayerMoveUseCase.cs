using Soroeru.InGame.Data.DataStore;
using Soroeru.InGame.Domain.Repository;
using UnityEngine;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class PlayerMoveUseCase
    {
        private readonly Rigidbody2D _rigidbody2d;
        private readonly PlayerMoveData _moveData;
        private bool _isPowerUp;

        public PlayerMoveUseCase(Rigidbody2D rigidbody2D, PlayerRepository playerRepository)
        {
            _rigidbody2d = rigidbody2D;
            _moveData = playerRepository.GetMoveData();
            _isPowerUp = false;
        }

        public void SetVelocityX(float x)
        {
            _rigidbody2d.velocity = _moveData.GetMoveSpeed(_isPowerUp) * x * Vector2.right + GetGravityVector();
        }

        public void SetPowerUp(bool isPowerUp)
        {
            _isPowerUp = isPowerUp;
        }

        private Vector2 GetGravityVector()
        {
            return new Vector2(0.0f, gravity);
        }

        public float gravity => _rigidbody2d.velocity.y;

        public void Jump()
        {
            Jump(_moveData.jump);
        }

        public void Jump(float jumpPower)
        {
            _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, 0) + jumpPower * Vector2.up;
        }

        public void Tick(float deltaTime, bool inputKey)
        {
            if (_rigidbody2d.velocity.y < 0 ||
                _rigidbody2d.velocity.y > 0 && !inputKey)
            {
                _rigidbody2d.velocity += Physics2D.gravity.y * deltaTime * 0.5f * Vector2.up;
            }
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