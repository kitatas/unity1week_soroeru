using TMPro;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class CoinCountView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinCountText = default;

        public void Display(int value)
        {
            coinCountText.text = $"{value}";
        }
    }
}