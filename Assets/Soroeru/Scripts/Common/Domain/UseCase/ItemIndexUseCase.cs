using EFUK;
using UniRx;

namespace Soroeru.Common.Domain.UseCase
{
    public sealed class ItemIndexUseCase
    {
        private readonly ReactiveProperty<int> _index;

        public ItemIndexUseCase()
        {
            _index = new ReactiveProperty<int>(0);
        }

        public IReadOnlyReactiveProperty<int> index => _index;

        private void Set(int setValue)
        {
            _index.Value = setValue;
        }

        public void ResetValue()
        {
            Set(0);
        }

        public void RepeatIncrement(int length)
        {
            Set(MathfExtension.RepeatIncrement(value, 0, length));
        }

        public void RepeatDecrement(int length)
        {
            Set(MathfExtension.RepeatDecrement(value, 0, length));
        }

        public int value => _index.Value;
    }
}