namespace Soroeru.OutGame.Domain.UseCase
{
    public interface IInputUseCase
    {
        int verticalDown { get; }
        int horizontalDown { get; }
        float vertical { get; }
        bool isDecision { get; }
        bool isBack { get; }
        bool isAnyKey { get; }
    }
}