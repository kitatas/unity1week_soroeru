using EFUK;
using UnityEngine;

namespace Soroeru.OutGame.Presentation.View
{
    public sealed class MenuView : MonoBehaviour
    {
        [SerializeField] private RectTransform cursor = default;
        [SerializeField] private MenuItemView[] items = default;

        public void SetCursorPosition(int index)
        {
            cursor.localPosition = items[index].localPosition;
        }

        public ScreenType GetCurrentType(int index)
        {
            return items[index].type.ConvertScreenType();
        }

        public int itemLastIndex => items.GetLastIndex();
    }
}