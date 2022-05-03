using UnityEngine;

namespace Soroeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(EquipData), menuName = "DataTable/" + nameof(EquipData), order = 0)]
    public sealed class EquipData : ScriptableObject
    {
        [SerializeField] private EquipType equipType = default;
        [SerializeField] private Sprite equipSprite = default;
        [SerializeField] private float lifeTime = default;

        public EquipType type => equipType;
        public Sprite sprite => equipSprite;
        public float time => lifeTime;
    }
}