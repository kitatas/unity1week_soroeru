using UniRx;
using UnityEngine;

namespace Soroeru.Common.Domain.UseCase
{
    public sealed class TimeUseCase
    {
        private readonly ReactiveProperty<bool> _isStop;

        public TimeUseCase()
        {
            _isStop = new ReactiveProperty<bool>(false);
        }

        public IReadOnlyReactiveProperty<bool> isStop => _isStop;

        public bool isPause => _isStop.Value;

        public void SetPause()
        {
            _isStop.Value = !_isStop.Value;
            Time.timeScale = isPause ? 0.0f : 1.0f;
        }

        public void CancelPause()
        {
            _isStop.Value = false;
            Time.timeScale = 1.0f;
        }
    }
}