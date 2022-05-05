using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class BombDamageView : DamageView
    {
        private void Start()
        {
            this.OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    Debug.Log($"collision enter");
                    if (other.gameObject.TryGetComponent(out EnemyView enemyView))
                    {
                        Debug.Log($"{enemyView}");
                        enemyView.ApplyDamage(power);
                    }
                })
                .AddTo(this);
        }

        public void SetUp(float lifeTime)
        {
            Destroy(gameObject, lifeTime);

            transform
                .DOScale(Vector3.one * SlotItemConfig.BOMB_EXPLODE_SIZE, lifeTime)
                .SetLink(gameObject);
        }
    }
}