using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public abstract class BaseAttackCollision : MonoBehaviour
    {
        [SerializeField] protected float lifeTime = default;

        public abstract void Fire(Transform owner, Direction direction);
    }
}