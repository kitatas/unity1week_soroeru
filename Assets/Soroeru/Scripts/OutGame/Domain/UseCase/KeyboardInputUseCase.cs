using EFUK;
using Soroeru.Common;
using UnityEngine;

namespace Soroeru.OutGame.Domain.UseCase
{
    public sealed class KeyboardInputUseCase : IInputUseCase
    {
        public int verticalDown => InputExtension.GetAxisDownValue(InputAxisConfig.VERTICAL);
        public int horizontalDown => InputExtension.GetAxisDownValue(InputAxisConfig.HORIZONTAL);
        public float vertical => Input.GetAxisRaw(InputAxisConfig.VERTICAL);

        public bool isDecision => Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space);
        public bool isBack => Input.GetKeyDown(KeyCode.Escape);
        public bool isAnyKey => Input.anyKeyDown;
    }
}