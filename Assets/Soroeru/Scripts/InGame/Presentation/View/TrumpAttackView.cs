using System.Collections.Generic;
using Soroeru.InGame.Data.Entity;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class TrumpAttackView : BaseAttackCollision
    {
        [SerializeField] private List<CardAttackView> cardAttackViews = default;

        public override void Equip(AttackEntity attackEntity)
        {
            transform.SetParent(attackEntity.owner);

            foreach (var cardAttackView in cardAttackViews)
            {
                cardAttackView.Init(attackEntity.owner, attackEntity.attackPower);
            }
        }

        public override void Fire(AttackEntity attackEntity)
        {
            foreach (var cardAttackView in cardAttackViews)
            {
                cardAttackView.Spread();
            }
        }
    }
}