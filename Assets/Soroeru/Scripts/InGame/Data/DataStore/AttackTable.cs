using System.Collections.Generic;
using UnityEngine;

namespace Soroeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(AttackTable), menuName = "DataTable/" + nameof(AttackTable), order = 0)]
    public sealed class AttackTable : ScriptableObject
    {
        [SerializeField] private List<AttackData> dataList = default;

        public List<AttackData> list => dataList;
    }
}