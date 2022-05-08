using Soroeru.Common;
using UnityEngine;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class KeyboardInputUseCase : IPlayerInputUseCase
    {
        public float horizontal => Input.GetAxisRaw(InputAxisConfig.HORIZONTAL);
        public float vertical => Input.GetAxisRaw(InputAxisConfig.VERTICAL);
        public bool isJump => Input.GetKeyDown(KeyCode.Space);
        public bool isJumping => Input.GetKey(KeyCode.Space);
        public bool isAttack => Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return);
        public bool isMenu => Input.GetKeyDown(KeyCode.Tab);
    }
}