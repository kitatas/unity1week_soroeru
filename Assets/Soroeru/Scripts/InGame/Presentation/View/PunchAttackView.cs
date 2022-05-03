using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class PunchAttackView : BaseAttackCollision
    {
        [SerializeField] private float lifeTime = default;

        public override void Fire(Transform owner, Direction direction)
        {
            transform.SetParent(owner);

            Destroy(gameObject, lifeTime);
        }
    }
}