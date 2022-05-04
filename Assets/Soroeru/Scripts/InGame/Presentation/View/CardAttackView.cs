using EFUK;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class CardAttackView : BaseAttackCollision
    {
        [SerializeField] private Transform lastTarget = default;
        private bool _isSpread;

        public void Init(Transform owner, int attackPower)
        {
            _isSpread = false;

            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var deltaTime = Time.deltaTime;
                    GetCloserOrSpread(owner, deltaTime);
                    RotateAround(owner, deltaTime);
                })
                .AddTo(this);

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.gameObject.TryGetComponent(out EnemyView enemyView))
                    {
                        enemyView.ApplyDamage(attackPower);
                    }
                })
                .AddTo(this);
        }

        private void RotateAround(Transform owner, float deltaTime)
        {
            transform.RotateAround(
                owner.position,
                Vector3.forward,
                -180.0f * deltaTime);

            lastTarget.RotateAround(
                owner.position,
                Vector3.forward,
                -180.0f * deltaTime);

            transform.eulerAngles = Vector3.zero;
        }

        private void GetCloserOrSpread(Transform owner, float deltaTime)
        {
            if (_isSpread)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    lastTarget.position,
                    deltaTime);
                return;
            }

            if (transform.position.GetSqrLength(owner.position) > 0.3f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    owner.position,
                    deltaTime);
            }
        }

        public void Spread()
        {
            _isSpread = true;
            this.Delay(1.0f, () => _isSpread = false);
        }
    }
}