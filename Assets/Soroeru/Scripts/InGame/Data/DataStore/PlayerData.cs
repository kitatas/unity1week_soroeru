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
        [SerializeField] private float powerUpMoveSpeed = default;
        [SerializeField] private float jumpPower = default;

        public float jump => jumpPower;

        public float GetMoveSpeed(bool isPowerUp)
        {
            return isPowerUp ? powerUpMoveSpeed : moveSpeed;
        }
    }
}