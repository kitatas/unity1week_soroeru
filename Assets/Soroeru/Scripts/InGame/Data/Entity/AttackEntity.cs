using UnityEngine;

namespace Soroeru.InGame.Data.Entity
{
    public struct AttackEntity
    {
        public readonly Transform owner;
        public readonly Direction direction;
        public readonly float lifeTime;
        public readonly int attackPower;

        public AttackEntity(Transform owner, Direction direction, float lifeTime, int attackPower)
        {
            this.owner = owner;
            this.direction = direction;
            this.lifeTime = lifeTime;
            this.attackPower = attackPower;
        }
    }
}