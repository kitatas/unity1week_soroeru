using System.Collections.Generic;
using UnityEngine;

namespace Soroeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BuffTable), menuName = "DataTable/" + nameof(BuffTable), order = 0)]
    public sealed class BuffTable : ScriptableObject
    {
        [SerializeField] private List<BuffData> dataList = default;

        public List<BuffData> list => dataList;
    }
}