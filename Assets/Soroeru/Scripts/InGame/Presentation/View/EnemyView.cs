using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public class EnemyView : DamageView
    {
        [SerializeField] private int hitPoint = default;

        [SerializeField] private EnemyAmmoView enemyAmmoView = default;
        [SerializeField] private float shotInterval = default;

        protected Vector3 initPosition;

        private void Start()
        {
            StartCoroutine(Shot());

            initPosition = transform.position;

            this.UpdateAsObservable()
                .Subscribe(_ => Tick())
                .AddTo(this);
        }

        private IEnumerator Shot()
        {
            var interval = new WaitForSeconds(shotInterval);
            while (true)
            {
                yield return interval;

                var ammo = Instantiate(enemyAmmoView, transform.position, Quaternion.identity);
                ammo.Fire();
            }
        }

        protected virtual void Tick()
        {
        }

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