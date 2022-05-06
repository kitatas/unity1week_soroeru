using Soroeru.InGame.Domain.UseCase;
using Soroeru.InGame.Presentation.View;
using UniRx;
using UniRx.Triggers;
using VContainer.Unity;

namespace Soroeru.InGame.Presentation.Controller
{
    public sealed class CameraController : IInitializable
    {
        private readonly EnemyUseCase _enemyUseCase;
        private readonly CameraView _cameraView;
        private readonly PlayerView _playerView;

        public CameraController(EnemyUseCase enemyUseCase, CameraView cameraView, PlayerView playerView)
        {
            _enemyUseCase = enemyUseCase;
            _cameraView = cameraView;
            _playerView = playerView;
        }

        public void Initialize()
        {
            _cameraView.Init(_enemyUseCase.SetUp);

            _cameraView.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    _cameraView.Tick(_playerView.direction);
                })
                .AddTo(_cameraView);
        }
    }
}