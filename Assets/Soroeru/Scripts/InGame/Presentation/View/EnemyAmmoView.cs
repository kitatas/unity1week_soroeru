using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class EnemyAmmoView : DamageView
    {
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private float lifeTime = default;

        public void Fire()
        {
            Destroy(gameObject, lifeTime);
            GetComponent<Rigidbody2D>().velocity = moveSpeed * Vector2.left;

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    // TODO: 他の敵に当たった時
                    // TODO: 爆弾に当たった時
                    if (!other.TryGetComponent(out EnemyView enemyView))
                    {
                        Destroy(gameObject);
                    }
                })
                .AddTo(this);

        }
    }
}