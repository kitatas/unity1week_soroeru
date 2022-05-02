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
        private float _startPositionY;
        private float _endPositionY;

        public float height => transform.position.y;

        public void Init(float moveSpeed, float startPositionY, float endPositionY)
        {
            _moveSpeed = moveSpeed;
            _startPositionY = startPositionY;
            _endPositionY = endPositionY;
        }

        public void Tick(float deltaTime)
        {
            transform.TranslateY(_moveSpeed * deltaTime * -1);

            if (height <= _endPositionY)
            {
                transform.TranslateY(_startPositionY - _endPositionY);
            }
        }

        /// <summary>
        /// 位置の補正
        /// </summary>
        public void Correct()
        {
            var y = Mathf.RoundToInt(height);
            transform
                .DOLocalMoveY(y, 0.05f)
                .SetEase(Ease.Linear);
        }
    }
}