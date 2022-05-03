using System.Collections.Generic;
using UnityEngine;

namespace Soroeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(EquipTable), menuName = "DataTable/" + nameof(EquipTable), order = 0)]
    public sealed class EquipTable : ScriptableObject
    {
        [SerializeField] private List<EquipData> dataList = default;

        public List<EquipData> list => dataList;
    }
}