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

        // TODO: 仮の初期化なので修正する
        private void Start()
        {
            Init();

            // リールの回転
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var deltaTime = Time.deltaTime;
                    Tick(deltaTime);
                })
                .AddTo(this);

            // リールの停止
            int index = 0;
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Space))
                .Subscribe(_ =>
                {
                    if (reelViews.TryGetValue(index, out var reelView))
                    {
                        reelView.Stop();
                        index++;
                    }
                })
                .AddTo(this);

            // 出目の役
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Q))
                .Subscribe(_ =>
                {
                    LogRoleAll();
                })
                .AddTo(this);

            // 再度回転
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.Return))
                .Subscribe(_ =>
                {
                    index = 0;
                    StartRollAll();
                })
                .AddTo(this);
        }

        public void Init()
        {
            var moveSpeed = 5.0f;
            var startPositionY = 4.0f;
            var endPositionY = -4.0f;

            foreach (var reelView in reelViews)
            {
                reelView.Init(moveSpeed, startPositionY, endPositionY);
            }
        }

        public void Tick(float deltaTime)
        {
            foreach (var reelView in reelViews)
            {
                reelView.Tick(deltaTime);
            }
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
            foreach (var reelView in reelViews)
            {
                reelView.StartRoll();
            }
        }
    }
}