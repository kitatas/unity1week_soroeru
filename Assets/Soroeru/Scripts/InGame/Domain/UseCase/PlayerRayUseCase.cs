using UnityEngine;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class PlayerRayUseCase
    {
        private readonly Transform _transform;
        private readonly int _layerMask;

        private const float DISTANCE = 0.35f;

        public PlayerRayUseCase(Transform transform)
        {
            _transform = transform;
            _layerMask = 1 << LayerMask.NameToLayer(LayerConfig.GROUND);
        }

        public bool IsGround()
        {
            var hit = Physics2D.Raycast(_transform.position, Vector2.down, 100, _layerMask);
            return hit.distance < DISTANCE;
        }
    }
}