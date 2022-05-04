using System.Collections;
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

        public IEnumerator Flash(float time)
        {
            var interval = 0.1f;
            var intervalTime = new WaitForSeconds(interval);
            while (time > 0)
            {
                _spriteRenderer.enabled = false;
                yield return intervalTime;

                _spriteRenderer.enabled = true;
                yield return intervalTime;

                time -= interval * 2;
            }
        }
    }
}