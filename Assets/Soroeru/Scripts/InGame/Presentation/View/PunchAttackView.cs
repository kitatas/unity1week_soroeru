using Soroeru.Common;
using Soroeru.InGame.Data.Entity;
using UniRx;
using UniRx.Triggers;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class PunchAttackView : BaseAttackCollision
    {
        public override void Fire(AttackEntity attackEntity)
        {
            Destroy(gameObject, attackEntity.lifeTime);

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.gameObject.TryGetComponent(out EnemyView enemyView))
                    {
                        seController.Play(SeType.HitEnemy);
                        enemyView.ApplyDamage(attackEntity.attackPower);
                    }

                    Destroy(gameObject);
                })
                .AddTo(this);
        }
    }
}