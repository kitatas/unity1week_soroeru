using UnityEngine;

namespace Soroeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BuffData), menuName = "DataTable/" + nameof(BuffData), order = 0)]
    public sealed class BuffData : ScriptableObject
    {
        [SerializeField] private BuffType buffType = default;
        [SerializeField] private int buffValue;

        public BuffType type => buffType;
        public int value => buffValue;
    }
}