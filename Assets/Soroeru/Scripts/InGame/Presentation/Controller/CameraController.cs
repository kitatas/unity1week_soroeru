using Soroeru.InGame.Domain.UseCase;
using Soroeru.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Soroeru.InGame.Presentation.Controller
{
    public sealed class CameraController : IInitializable
    {
        private readonly EnemyUseCase _enemyUseCase;
        private readonly PlayerDirectionUseCase _playerDirectionUseCase;
        private readonly CameraView _cameraView;

        public CameraController(EnemyUseCase enemyUseCase, PlayerDirectionUseCase playerDirectionUseCase,
            CameraView cameraView)
        {
            _enemyUseCase = enemyUseCase;
            _playerDirectionUseCase = playerDirectionUseCase;
            _cameraView = cameraView;
        }

        public void Initialize()
        {
            _cameraView.Init(popRange =>
            {
                popRange
                    .OnTriggerEnter<EnemyPopView>(x =>
                    {
                        if (x.instance)
                        {
                            return;
                        }

                        var enemy = _enemyUseCase.SetUp(x.type, x.position);
                        x.SetInstance(enemy);
                    });

                popRange
                    .OnTriggerExit<EnemyPopView>(x =>
                    {
                        x.DestroyInstance();
                    });
            });

            _playerDirectionUseCase.direction
                .Subscribe(_cameraView.Tick)
                .AddTo(_cameraView);
        }
    }
}