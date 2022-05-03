using EFUK;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class PlayerEquipView : MonoBehaviour
    {
        [SerializeField] private Image equipImage = default;
        [SerializeField] private TextMeshProUGUI lifeTimeText = default;

        public void SetSprite(Sprite sprite)
        {
            equipImage.sprite = sprite;
        }

        public void SetLifeTime(float lifeTime)
        {
            lifeTimeText.text = lifeTime.EqualZero() ? $"---" : $"{lifeTime: 0.00}";
        }
    }
}