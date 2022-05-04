using System;
using System.Collections.Generic;
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
        public int pictureCount => pictureViews.Count;

        public void Init(float moveSpeed)
        {
            StartRoll();

            for (int i = 0; i < pictureCount; i++)
            {
                var y = (pictureCount / 2.0f) - (i + 1);
                pictureViews[i].Init(moveSpeed, new Vector3(0.0f, y, 0.0f));
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

        public PictureType GetHitPictureType()
        {
            foreach (var pictureView in pictureViews)
            {
                if (Mathf.RoundToInt(pictureView.localHeight) == 0)
                {
                    return pictureView.type;
                }
            }

            throw new Exception($"invalid reel picture.");
        }

        public void StartRoll()
        {
            isStop = false;
        }
    }
}