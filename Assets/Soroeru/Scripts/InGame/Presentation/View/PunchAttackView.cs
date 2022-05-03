using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class PunchAttackView : BaseAttackCollision
    {
        public override void Fire(Transform owner, Direction direction)
        {
            transform.SetParent(owner);

            Destroy(gameObject, lifeTime);
        }
    }
}