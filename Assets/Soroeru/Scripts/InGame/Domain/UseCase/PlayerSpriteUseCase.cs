using EFUK;
using UnityEngine;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class PlayerSpriteUseCase
    {
        private readonly SpriteRenderer _spriteRenderer;

        public PlayerSpriteUseCase(SpriteRenderer spriteRenderer)
        {
            _spriteRenderer = spriteRenderer;
        }

        public void Flip(float value)
        {
            if (value.EqualZero())
            {
                return;
            }

            _spriteRenderer.flipX = value <= 0.0f;
        }
    }
}