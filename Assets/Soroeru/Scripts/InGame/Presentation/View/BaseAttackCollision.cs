using Soroeru.Common.Presentation.Controller;
using Soroeru.InGame.Data.Entity;
using UnityEngine;
using VContainer.Unity;

namespace Soroeru.InGame.Presentation.View
{
    public abstract class BaseAttackCollision : MonoBehaviour
    {
        protected SeController seController;

        private void Start()
        {
            var resolver = VContainerSettings.Instance.RootLifetimeScope.Container;
            seController = resolver.Resolve(typeof(SeController)) as SeController;
        }

        public virtual void Equip(AttackEntity attackEntity)
        {
        }

        public virtual void Fire(AttackEntity attackEntity)
        {
        }
    }
}