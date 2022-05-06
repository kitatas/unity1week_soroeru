using Soroeru.InGame.Data.Entity;
using UniRx;
using UniRx.Triggers;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class ArmorAttackView : BaseAttackCollision
    {
        public override void Fire(AttackEntity attackEntity)
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    transform.position = attackEntity.owner.position;
                })
                .AddTo(this);

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.gameObject.TryGetComponent(out EnemyView enemyView))
                    {
                        enemyView.ApplyDamage(attackEntity.attackPower);
                    }
                })
                .AddTo(this);
        }
    }
}