using UnityEngine;

namespace Soroeru.Common.Presentation.View
{
    public sealed class PauseItemView : MonoBehaviour
    {
        [SerializeField] private PauseItemType pauseItemType = default;

        public PauseItemType type => pauseItemType;
        public Vector3 localPosition => transform.localPosition;
    }
}