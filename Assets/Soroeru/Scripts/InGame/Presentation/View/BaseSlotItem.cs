using Soroeru.Common.Presentation.Controller;
using UnityEngine;
using VContainer.Unity;

namespace Soroeru.InGame.Presentation.View
{
    public abstract class BaseSlotItem : MonoBehaviour
    {
        protected SeController seController;
        
        protected virtual void Start()
        {
            var resolver = VContainerSettings.Instance.RootLifetimeScope.Container;
            seController = resolver.Resolve(typeof(SeController)) as SeController;
        }

        public virtual void Generate(float lifeTime)
        {
            Destroy(gameObject, lifeTime);
        }
    }
}