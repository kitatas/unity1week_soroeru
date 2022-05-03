using System;
using UnityEngine;

namespace Soroeru.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(PlayerData), menuName = "DataTable/" + nameof(PlayerData), order = 0)]
    public sealed class PlayerData : ScriptableObject
    {
        [SerializeField] private PlayerMoveData playerMoveData = default;

        public PlayerMoveData moveData => playerMoveData;
    }

    [Serializable]
    public sealed class PlayerMoveData
    {
        
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private float lowJumpPower = default;
        [SerializeField] private float highJumpPower = default;

        public float speed => moveSpeed;
        public float lowJump => lowJumpPower;
        public float highJump => highJumpPower;
    }
}