using Soroeru.InGame.Presentation.View;
using UnityEngine;

namespace Soroeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(ItemTable), menuName = "DataTable/" + nameof(ItemTable), order = 0)]
    public sealed class ItemTable : ScriptableObject
    {
        [SerializeField] private CoinView coinView;

        public CoinView coin => coinView;
    }
}