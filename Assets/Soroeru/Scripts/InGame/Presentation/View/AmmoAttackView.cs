using Soroeru.InGame.Data.Entity;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class AmmoAttackView : BaseAttackCollision
    {
        [SerializeField] private float moveSpeed = default;

        public override void Fire(AttackEntity attackEntity)
        {
            base.Fire(attackEntity);

            GetComponent<Rigidbody2D>().velocity = moveSpeed * attackEntity.direction.ConvertVector2();
            GetComponent<SpriteRenderer>().flipX = attackEntity.direction == Direction.Left;
        }
    }
}