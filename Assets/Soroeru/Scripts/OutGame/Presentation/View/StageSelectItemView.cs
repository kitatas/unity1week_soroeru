using Soroeru.Common;
using UnityEngine;

namespace Soroeru.OutGame.Presentation.View
{
    public sealed class StageSelectItemView : MonoBehaviour
    {
        [SerializeField] private SceneName sceneName = default;

        public SceneName scene => sceneName;
        public Vector3 localPosition => transform.localPosition;
    }
}