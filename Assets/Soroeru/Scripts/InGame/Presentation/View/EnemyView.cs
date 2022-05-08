using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public class EnemyView : DamageView
    {
        [SerializeField] private int hitPoint = default;
        [SerializeField] private EnemyExplodeView explode = default;

        private Renderer _renderer;
        public bool isVisible => _renderer.isVisible;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

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
                Destroy(gameObject);
                Instantiate(explode, transform.position, Quaternion.identity);
            }
        }
    }
}