using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class EnemyPopView : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType = default;
        public EnemyType type => enemyType;
        public Vector3 position => transform.position;

        public EnemyView instance { get; private set; }

        public void SetInstance(EnemyView enemyView)
        {
            instance = enemyView;
        }

        public void DestroyInstance()
        {
            if (instance)
            {
                if (instance.isVisible)
                {
                    return;
                }
                
                Destroy(instance.gameObject);
                instance = null;
            }
        }
    }
}