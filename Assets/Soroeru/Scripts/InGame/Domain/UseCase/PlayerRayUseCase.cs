using UnityEngine;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class PlayerRayUseCase
    {
        private readonly Transform _transform;

        private const float DISTANCE = 0.3f;

        public PlayerRayUseCase(Transform transform)
        {
            _transform = transform;
        }

        public bool IsGround()
        {
            var hit = Physics2D.Raycast(_transform.position, Vector2.down, DISTANCE);
            if (hit.collider == false)
            {
                return false;
            }

            return hit.collider.gameObject.layer == LayerMask.NameToLayer(LayerConfig.GROUND);
        }
    }
}