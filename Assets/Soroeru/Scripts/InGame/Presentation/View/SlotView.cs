using System.Linq;
using EFUK;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    /// <summary>
    /// スロット画面
    /// </summary>
    public sealed class SlotView : MonoBehaviour
    {
        [SerializeField] private ReelView[] reelViews = default;

        private readonly Vector3 _traceOffset = new Vector3(-0.55f, 0.6f, 0.0f);
        private int _reelIndex = 0;

        // TODO: 仮の初期化なので修正する
        private void Start()
        {
            // 出目の役
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Q))
                .Subscribe(_ =>
                {
                    LogRoleAll();
                })
                .AddTo(this);
        }

        public void Init()
        {
            _reelIndex = 0;
            var moveSpeed = 1.0f;
            foreach (var reelView in reelViews)
            {
                reelView.Init(moveSpeed);
            }
        }

        public void Tick(Transform player, float deltaTime)
        {
            transform.position = player.position + _traceOffset;

            var height = transform.position.y;
            var startPositionY = height + 0.8f;
            var endPositionY = height - 0.8f;
            foreach (var reelView in reelViews)
            {
                reelView.Tick(startPositionY, endPositionY, deltaTime);
            }
        }

        public void StopReel()
        {
            if (reelViews.TryGetValue(_reelIndex, out var reelView))
            {
                reelView.Stop();
                _reelIndex++;
            }
        }

        public bool IsReelStopAll()
        {
            return reelViews.All(x => x.isStop);
        }

        public void LogRoleAll()
        {
            foreach (var reelView in reelViews)
            {
                reelView.GetHitPicture();
            }
        }

        public void StartRollAll()
        {
            _reelIndex = 0;
            foreach (var reelView in reelViews)
            {
                reelView.StartRoll();
            }
        }
    }
}