using EFUK;
using UnityEngine;

namespace Soroeru.Common.Domain.UseCase
{
    public sealed class KeyboardInputUseCase : IInputUseCase
    {
        public int horizontalDown => InputExtension.GetAxisDownValue(InputAxisConfig.HORIZONTAL);
        public int verticalDown => InputExtension.GetAxisDownValue(InputAxisConfig.VERTICAL);
        public float horizontal => Input.GetAxisRaw(InputAxisConfig.HORIZONTAL);
        public float vertical => Input.GetAxisRaw(InputAxisConfig.VERTICAL);

        public bool isJump => Input.GetKeyDown(KeyCode.Space);
        public bool isJumping => Input.GetKey(KeyCode.Space);
        public bool isAttack => Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return);
        public bool isMenu => Input.GetKeyDown(KeyCode.Tab);

        public bool isDecision => Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space);
        public bool isBack => Input.GetKeyDown(KeyCode.Q);
        public bool isAnyKey => Input.anyKeyDown;
    }
}