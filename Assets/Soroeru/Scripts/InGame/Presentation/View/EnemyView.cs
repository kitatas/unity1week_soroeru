using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private int hitPoint = default;

        public void ApplyDamage(int damage)
        {
            hitPoint -= damage;
            if (hitPoint <= 0)
            {
                // TODO: 死亡演出
                Destroy(gameObject);
            }
        }
    }
}