using Soroeru.InGame.Data.Entity;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public abstract class BaseAttackCollision : MonoBehaviour
    {
        public virtual void Equip(AttackEntity attackEntity)
        {

        }

        public virtual void Fire(AttackEntity attackEntity)
        {
            Destroy(gameObject, attackEntity.lifeTime);

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.gameObject.TryGetComponent(out EnemyView enemyView))
                    {
                        enemyView.ApplyDamage(attackEntity.attackPower);
                    }

                    Destroy(gameObject);
                })
                .AddTo(this);
        }
    }
}