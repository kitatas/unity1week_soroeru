using EFUK;
using UnityEngine;

namespace Soroeru.Common.Domain.UseCase
{
    public sealed class KeyboardInputUseCase : IInputUseCase
    {
        public int verticalDown => InputExtension.GetAxisDownValue(InputAxisConfig.VERTICAL);
        public bool isDecision => Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space);
    }
}