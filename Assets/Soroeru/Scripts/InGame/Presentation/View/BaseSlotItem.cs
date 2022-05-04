using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public abstract class BaseSlotItem : MonoBehaviour
    {
        public virtual void Generate(float lifeTime)
        {
            Destroy(gameObject, lifeTime);
        }
    }
}