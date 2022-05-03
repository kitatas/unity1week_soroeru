using Soroeru.InGame.Presentation.View;
using UnityEngine;

namespace Soroeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(AttackData), menuName = "DataTable/" + nameof(AttackData), order = 0)]
    public sealed class AttackData : ScriptableObject
    {
        [SerializeField] private EquipType equipType = default;
        [SerializeField] private BaseAttackCollision attackCollision = default;
        [SerializeField] private float lifeTime = default;
        [SerializeField] private int attackPower = default;

        public EquipType type => equipType;
        public BaseAttackCollision collision => attackCollision;
        public float time => lifeTime;
        public int power => attackPower;
    }
}