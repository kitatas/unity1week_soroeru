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

        public void Init(float moveSpeed)
        {
            foreach (var pictureView in pictureViews)
            {
                pictureView.Init(moveSpeed);
            }
        }

        public void Tick(float startPositionY, float endPositionY, float deltaTime)
        {
            if (_isStop)
            {
                return;
            }

            foreach (var pictureView in pictureViews)
            {
                pictureView.Tick(startPositionY, endPositionY, deltaTime);
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
                if (pictureView.localHeight.EqualZero())
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