using UnityEngine;

namespace Soroeru.OutGame.Presentation.View
{
    public sealed class MenuItemView : MonoBehaviour
    {
        [SerializeField] private MenuItemType menuItemType = default;

        public MenuItemType type => menuItemType;
        public Vector3 localPosition => transform.localPosition;
    }
}