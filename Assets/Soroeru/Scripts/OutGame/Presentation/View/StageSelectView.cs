using System;
using EFUK;
using Soroeru.Common;
using UnityEngine;

namespace Soroeru.OutGame.Presentation.View
{
    public sealed class StageSelectView : MonoBehaviour
    {
        [SerializeField] private RectTransform cursor = default;
        [SerializeField] private StageSelectItemView[] items = default;

        public void SetCursorPosition(int index)
        {
            cursor.localPosition = items[index].localPosition;
        }

        public SceneName GetSceneName(int index)
        {
            return items[index].scene;
        }

        public int itemLastIndex => items.GetLastIndex();

        public void ResetView(Action action)
        {
            this.Delay(UiConfig.POP_UP_ANIMATION_TIME, () => action?.Invoke());
        }
    }
}