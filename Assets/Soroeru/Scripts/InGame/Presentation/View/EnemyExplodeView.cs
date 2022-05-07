using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class EnemyExplodeView : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, 1.0f);
        }
    }
}