namespace Soroeru.InGame.Domain.UseCase
{
    public interface IPlayerInputUseCase
    {
        float horizontal { get; }
        float vertical { get; }
        bool isJump { get; }
        bool isJumping { get; }
        bool isAttack { get; }
        bool isDecision { get; }
        bool isMenu { get; }
    }
}