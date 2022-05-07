using UnityEngine;
using UnityEngine.UI;

namespace Soroeru.OutGame.Presentation.View
{
    public sealed class OptionItemView : MonoBehaviour
    {
        [SerializeField] private OptionType optionType = default;
        [SerializeField] private Image effectValue = default;

        public OptionType type => optionType;
        public Vector3 localPosition => transform.localPosition;

        public void SetEffectValue(float value)
        {
            effectValue.fillAmount = value;
        }
    }
}