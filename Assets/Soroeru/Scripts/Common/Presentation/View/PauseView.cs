using EFUK;
using UniRx;
using UnityEngine;

namespace Soroeru.Common.Presentation.View
{
    public sealed class PauseView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup = default;
        [SerializeField] private RectTransform cursor = default;
        [SerializeField] private PauseItemView[] items = default;
        private ReactiveProperty<int> _index;

        private int index => _index.Value;
        public PauseItemType type => items[index].type;

        public void Init()
        {
            _index = new ReactiveProperty<int>(0);
            _index
                .Subscribe(x =>
                {
                    cursor.transform.localPosition = items[x].localPosition;
                })
                .AddTo(this);

            Hide();
        }

        public void CursorUp()
        {
            _index.Value = MathfExtension.RepeatDecrement(index, 0, items.GetLastIndex());
        }

        public void CursorDown()
        {
            _index.Value = MathfExtension.RepeatIncrement(index, 0, items.GetLastIndex());
        }

        public void Show()
        {
            canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
            _index.Value = 0;
        }
    }
}