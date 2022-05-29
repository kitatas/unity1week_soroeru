using EFUK;
using UnityEngine;

namespace Soroeru.Common.Presentation.View
{
    public sealed class PauseView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private RectTransform cursor = default;
        [SerializeField] private PauseItemView[] items = default;

        public void SetCursorPosition(int index)
        {
            cursor.transform.localPosition = items[index].localPosition;
        }

        public PauseItemType GetCurrentType(int index)
        {
            return items[index].type;
        }

        public int itemLastIndex => items.GetLastIndex();

        public void Show()
        {
            canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
        }
    }
}