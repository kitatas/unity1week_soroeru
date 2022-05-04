using System;
using EFUK;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class CoinView : MonoBehaviour
    {
        [SerializeField] private int value = default;

        public void PickUp(Action<int> action)
        {
            gameObject.SetActive(false);

            action?.Invoke(value);
        }

        public void Drop()
        {
            Destroy(gameObject, ItemConfig.DROP_COIN_LIFE_TIME);

            gameObject.SetLayer(LayerConfig.DROP);
            var x = Random.Range(-1.0f, 1.0f);
            var y = Random.Range(0.0f, 1.0f);
            var blowVector = new Vector2(x, y).normalized;

            var rigidbody2d = gameObject.GetOrAddComponent<Rigidbody2D>();
            rigidbody2d.AddForce(blowVector * 100);

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.gameObject.layer == LayerMask.NameToLayer(LayerConfig.GROUND))
                    {
                        Destroy(rigidbody2d);
                        gameObject.SetLayer(LayerConfig.DEFAULT);
                    }
                })
                .AddTo(this);
        }
    }
}