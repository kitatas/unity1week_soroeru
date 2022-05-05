using System.Collections.Generic;
using UnityEngine;

namespace Soroeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SlotItemTable), menuName = "DataTable/" + nameof(SlotItemTable), order = 0)]
    public sealed class SlotItemTable : ScriptableObject
    {
        [SerializeField] private List<SlotItemData> dataList = default;

        public List<SlotItemData> list => dataList;
    }
}