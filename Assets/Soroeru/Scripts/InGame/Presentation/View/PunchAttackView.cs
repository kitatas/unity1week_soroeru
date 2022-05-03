using Soroeru.InGame.Data.Entity;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class PunchAttackView : BaseAttackCollision
    {
        public override void Fire(AttackEntity attackEntity)
        {
            base.Fire(attackEntity);

            transform.SetParent(attackEntity.owner);
        }
    }
}