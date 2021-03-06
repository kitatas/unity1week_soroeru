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
        [SerializeField] private PictureType pictureType = default;

        private float _moveSpeed;

        public float height => transform.position.y;
        public float localHeight => transform.localPosition.y;
        public PictureType type => pictureType;

        public void Init(float moveSpeed, Vector3 initPosition)
        {
            _moveSpeed = moveSpeed;
            transform.localPosition = initPosition;
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
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    var current = transform.localPosition;
                    if (current.y.Equal(-6.0f))
                    {
                        transform.localPosition = new Vector3(current.x, 6.0f, current.z);
                    }
                });
        }
    }
}