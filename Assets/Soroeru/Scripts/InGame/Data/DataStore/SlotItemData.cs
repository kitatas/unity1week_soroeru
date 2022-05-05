using Soroeru.InGame.Presentation.View;
using UnityEngine;

namespace Soroeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SlotItemData), menuName = "DataTable/" + nameof(SlotItemData), order = 0)]
    public sealed class SlotItemData : ScriptableObject
    {
        [SerializeField] private ItemType itemType = default;
        [SerializeField] private BaseSlotItem slotItem = default;
        [SerializeField] private float lifeTime = default;

        public ItemType type => itemType;
        public BaseSlotItem item => slotItem;
        public float time => lifeTime;
    }
}