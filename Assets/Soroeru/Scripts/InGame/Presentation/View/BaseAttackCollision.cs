using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public abstract class BaseAttackCollision : MonoBehaviour
    {
        public abstract void Fire(Transform owner, Direction direction);
    }
}