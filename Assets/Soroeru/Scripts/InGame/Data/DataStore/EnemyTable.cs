using System.Collections.Generic;
using UnityEngine;

namespace Soroeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(EnemyTable), menuName = "DataTable/" + nameof(EnemyTable), order = 0)]
    public sealed class EnemyTable : ScriptableObject
    {
        [SerializeField] private List<EnemyData> dataList = default;

        public List<EnemyData> list => dataList;
    }
}