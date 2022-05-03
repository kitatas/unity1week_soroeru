using DG.Tweening;
using EFUK;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    /// <summary>
    /// リール内に置かれる役の画像
    /// </summary>
    public sealed class PictureView : MonoBehaviour
    {
        private float _moveSpeed;

        public float height => transform.position.y;
        public float localHeight => transform.localPosition.y;

        public void Init(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }

        public void Tick(float startPositionY, float endPositionY, float deltaTime)
        {
            transform.TranslateY(_moveSpeed * deltaTime * -1);

            if (height <= endPositionY)
            {
                transform.TranslateY(startPositionY - endPositionY);
            }
        }

        /// <summary>
        /// 位置の補正
        /// </summary>
        public void Correct()
        {
            var y = Mathf.RoundToInt(localHeight);
            transform
                .DOLocalMoveY(y, 0.05f)
                .SetEase(Ease.Linear);
        }
    }
}