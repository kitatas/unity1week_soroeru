using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class AmmoAttackView : BaseAttackCollision
    {
        [SerializeField] private float moveSpeed = default;

        public override void Fire(Transform owner, Direction direction)
        {
            GetComponent<Rigidbody2D>().velocity = moveSpeed * direction.ConvertVector();

            Destroy(gameObject, lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            // TODO: 敵判定？
            {
                Destroy(gameObject);
            }
        }
    }
}