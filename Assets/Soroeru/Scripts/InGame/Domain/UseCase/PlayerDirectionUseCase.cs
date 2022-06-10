using EFUK;
using UniRx;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class PlayerDirectionUseCase
    {
        private readonly ReactiveProperty<Direction> _direction;

        public PlayerDirectionUseCase()
        {
            _direction = new ReactiveProperty<Direction>(Direction.Right);
        }

        public IReadOnlyReactiveProperty<Direction> direction => _direction;

        public void Set(float value)
        {
            if (value.EqualZero())
            {
                return;
            }

            _direction.Value = value > 0 ? Direction.Right : Direction.Left;
        }

        public Direction current => _direction.Value;
    }
}