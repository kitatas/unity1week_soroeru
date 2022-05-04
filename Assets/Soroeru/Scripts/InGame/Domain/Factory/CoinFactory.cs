using Soroeru.InGame.Presentation.View;
using UnityEngine;

namespace Soroeru.InGame.Domain.Factory
{
    public sealed class CoinFactory
    {
        public CoinView Generate(CoinView coinView, Vector3 position)
        {
            return Object.Instantiate(coinView, position, Quaternion.identity);
        }
    }
}