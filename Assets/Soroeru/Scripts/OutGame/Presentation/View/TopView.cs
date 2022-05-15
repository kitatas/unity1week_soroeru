using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Soroeru.OutGame.Presentation.View
{
    public sealed class TopView : MonoBehaviour
    {
        [SerializeField] private Graphic press = default;

        public void FlashPress()
        {
            press
                .DOFade(0.0f, 0.2f)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo)
                .SetLink(press.gameObject);
        }
    }
}