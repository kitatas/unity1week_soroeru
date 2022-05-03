using System.Collections.Generic;
using System.Linq;
using EFUK;
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
        private int _reelIndex;
        private List<PictureType> _roleList;

        public void Init()
        {
            _reelIndex = 0;
            _roleList = new List<PictureType>();
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

        public List<PictureType> GetRole()
        {
            // 一定時間後、再回転
            this.Delay(SlotConfig.REEL_ROTATE_INTERVAL, StartRollAll);

            // 出目の取得
            foreach (var reelView in reelViews)
            {
                _roleList.Add(reelView.GetHitPictureType());
            }

            return _roleList;
        }

        private void StartRollAll()
        {
            _reelIndex = 0;
            _roleList.Clear();
            foreach (var reelView in reelViews)
            {
                reelView.StartRoll();
            }
        }
    }
}