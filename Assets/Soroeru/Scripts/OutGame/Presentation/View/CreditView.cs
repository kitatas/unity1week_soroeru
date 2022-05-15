using EFUK;
using UnityEngine;
using UnityEngine.UI;

namespace Soroeru.OutGame.Presentation.View
{
    public sealed class CreditView : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect = default;

        public void Tick(float moveRate)
        {
            scrollRect.verticalNormalizedPosition += moveRate * 0.1f;
        }

        public void ResetPosition()
        {
            this.Delay(UiConfig.POP_UP_ANIMATION_TIME, () =>
            {
                scrollRect.verticalNormalizedPosition = 1.0f;
            });
        }
    }
}