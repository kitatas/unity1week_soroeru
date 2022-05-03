using System;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class CoinView : MonoBehaviour
    {
        [SerializeField] private int value = default;

        public void PickUp(Action<int> action)
        {
            gameObject.SetActive(false);

            action?.Invoke(value);
        }
    }
}