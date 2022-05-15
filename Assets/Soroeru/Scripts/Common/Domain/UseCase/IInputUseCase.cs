namespace Soroeru.Common.Domain.UseCase
{
    public interface IInputUseCase
    {
        int horizontalDown { get; }
        int verticalDown { get; }
        float horizontal { get; }
        float vertical { get; }

        bool isJump { get; }
        bool isJumping { get; }
        bool isAttack { get; }
        bool isMenu { get; }

        bool isDecision { get; }
        bool isBack { get; }
        bool isAnyKey { get; }
    }
}