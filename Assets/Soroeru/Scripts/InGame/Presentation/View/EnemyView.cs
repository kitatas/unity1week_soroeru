using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public class EnemyView : DamageView
    {
        [SerializeField] private int hitPoint = default;

        protected virtual void Start()
        {
            this.UpdateAsObservable()
                .Subscribe(_ => Tick())
                .AddTo(this);
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