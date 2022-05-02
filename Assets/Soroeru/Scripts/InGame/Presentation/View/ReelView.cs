using System.Collections.Generic;
using EFUK;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    /// <summary>
    /// スロット内の1リール
    /// </summary>
    public sealed class ReelView : MonoBehaviour
    {
        [SerializeField] private List<PictureView> pictureViews;

        private bool _isStop = false;

        public void Init(float moveSpeed, float startPositionY, float endPositionY)
        {
            foreach (var pictureView in pictureViews)
            {
                pictureView.Init(moveSpeed, startPositionY, endPositionY);
            }
        }

        public void Tick(float deltaTime)
        {
            if (_isStop)
            {
                return;
            }

            foreach (var pictureView in pictureViews)
            {
                pictureView.Tick(deltaTime);
            }
        }

        public void Stop()
        {
            _isStop = true;

            foreach (var pictureView in pictureViews)
            {
                pictureView.Correct();
            }
        }

        // TODO: 仮のログ出力
        public void GetHitPicture()
        {
            foreach (var pictureView in pictureViews)
            {
                if (pictureView.height.EqualZero())
                {
                    Debug.Log($"{name} - {pictureView.name}");
                    return;
                }
            }
        }

        public void StartRoll()
        {
            _isStop = false;
        }
    }
}