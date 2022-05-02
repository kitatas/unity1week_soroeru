using System.Collections.Generic;
using UnityEngine;

namespace Soroeru.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BgmTable), menuName = "DataTable/" + nameof(BgmTable), order = 0)]
    public sealed class BgmTable : ScriptableObject
    {
        [SerializeField] private List<BgmData> dataList = default;

        public List<BgmData> list => dataList;
    }
}