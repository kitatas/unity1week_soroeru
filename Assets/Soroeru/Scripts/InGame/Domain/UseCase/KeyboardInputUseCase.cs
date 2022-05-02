using UnityEngine;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class KeyboardInputUseCase : IPlayerInputUseCase
    {
        public float horizontal => Input.GetAxisRaw("Horizontal");
        public float vertical => Input.GetAxisRaw("Vertical");
        public bool isJump => Input.GetKeyDown(KeyCode.Space);
        public bool isAttack => Input.GetKeyDown(KeyCode.E);
        public bool isDecision => Input.GetKeyDown(KeyCode.Return);
        public bool isMenu => Input.GetKeyDown(KeyCode.Tab);
    }
}