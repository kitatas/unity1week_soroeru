using Soroeru.Common;
using Soroeru.InGame.Data.Entity;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class AmmoAttackView : BaseAttackCollision
    {
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private Collider2D body = default;
        [SerializeField] private Collider2D around = default;

        public override void Fire(AttackEntity attackEntity)
        {
            Destroy(gameObject, attackEntity.lifeTime);

            around.OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.TryGetComponent(out EnemyView enemyView))
                    {
                        seController.Play(SeType.HitEnemy);
                        enemyView.ApplyDamage(attackEntity.attackPower);
                    }

                    if (!other.TryGetComponent(out BombView bombView))
                    {
                        Destroy(gameObject);
                    }
                })
                .AddTo(this);

            GetComponent<Rigidbody2D>().velocity = moveSpeed * attackEntity.direction.ConvertVector2();
            GetComponent<SpriteRenderer>().flipX = attackEntity.direction == Direction.Left;
        }
    }
}