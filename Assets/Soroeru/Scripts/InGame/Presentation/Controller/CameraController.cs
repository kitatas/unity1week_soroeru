using Soroeru.InGame.Presentation.View;
using UniRx;
using UniRx.Triggers;
using VContainer.Unity;

namespace Soroeru.InGame.Presentation.Controller
{
    public sealed class CameraController : IInitializable
    {
        private readonly CameraView _cameraView;
        private readonly PlayerView _playerView;

        public CameraController(CameraView cameraView, PlayerView playerView)
        {
            _cameraView = cameraView;
            _playerView = playerView;
        }

        public void Initialize()
        {
            _cameraView.Init();
            _cameraView.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    _cameraView.Tick(_playerView.direction);
                })
                .AddTo(_cameraView);
        }
    }
}