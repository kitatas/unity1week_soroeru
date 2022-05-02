using System.Collections.Generic;
using UnityEngine;

namespace Soroeru.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SeTable), menuName = "DataTable/" + nameof(SeTable), order = 0)]
    public sealed class SeTable : ScriptableObject
    {
        [SerializeField] private List<SeData> dataList = default;

        public List<SeData> list => dataList;
    }
}