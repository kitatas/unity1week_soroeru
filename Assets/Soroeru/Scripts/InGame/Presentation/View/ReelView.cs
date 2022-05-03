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

        public bool isStop { get; private set; }

        public void Init(float moveSpeed)
        {
            StartRoll();
            foreach (var pictureView in pictureViews)
            {
                pictureView.Init(moveSpeed);
            }
        }

        public void Tick(float startPositionY, float endPositionY, float deltaTime)
        {
            if (isStop)
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
            isStop = true;
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
            isStop = false;
        }
    }
}