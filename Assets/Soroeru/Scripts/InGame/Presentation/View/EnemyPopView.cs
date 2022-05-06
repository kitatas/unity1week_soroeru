using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class EnemyPopView : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType = default;
        public EnemyType type => enemyType;
        public Vector3 position => transform.position;

        private EnemyView _instance;

        public void SetInstance(EnemyView enemyView)
        {
            _instance = enemyView;
        }

        public void DestroyInstance()
        {
            if (_instance)
            {
                Destroy(_instance.gameObject);
            }
        }
    }
}