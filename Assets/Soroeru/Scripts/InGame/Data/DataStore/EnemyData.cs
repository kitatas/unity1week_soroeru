using Soroeru.InGame.Presentation.View;
using UnityEngine;

namespace Soroeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(EnemyData), menuName = "DataTable/" + nameof(EnemyData), order = 0)]
    public sealed class EnemyData : ScriptableObject
    {
        [SerializeField] private EnemyType enemyType = default;
        [SerializeField] private EnemyView enemyView = default;

        public EnemyType type => enemyType;
        public EnemyView enemy => enemyView;
    }
}