using System;
using EFUK;
using UnityEngine;

namespace Soroeru.OutGame.Presentation.View
{
    public sealed class OptionView : MonoBehaviour
    {
        [SerializeField] private RectTransform cursor = default;
        [SerializeField] private OptionItemView[] items = default;

        public void SetCursorPosition(int index)
        {
            cursor.localPosition = items[index].localPosition;
        }

        public OptionType GetCurrentType(int index)
        {
            return items[index].type;
        }

        public int itemLastIndex => items.GetLastIndex();

        public void SetVolume(int index, float volume)
        {
            items[index].SetEffectValue(volume);
        }

        public void ResetView(Action action)
        {
            this.Delay(UiConfig.POP_UP_ANIMATION_TIME, () => action?.Invoke());
        }
    }
}