using System;
using Soroeru.Common.Domain.UseCase;
using Soroeru.Common.Presentation.View;
using UniRx;
using UniRx.Triggers;
using VContainer.Unity;

namespace Soroeru.Common.Presentation.Controller
{
    public sealed class PauseController : IInitializable
    {
        private readonly IInputUseCase _inputUseCase;
        private readonly TimeUseCase _timeUseCase;
        private readonly SeController _seController;
        private readonly SceneLoader _sceneLoader;
        private readonly PauseView _pauseView;

        public PauseController(IInputUseCase inputUseCase, TimeUseCase timeUseCase,
            SeController seController, SceneLoader sceneLoader, PauseView pauseView)
        {
            _inputUseCase = inputUseCase;
            _timeUseCase = timeUseCase;
            _seController = seController;
            _sceneLoader = sceneLoader;
            _pauseView = pauseView;
        }

        public void Initialize()
        {
            _pauseView.Init();
            _pauseView.UpdateAsObservable()
                .Where(_ => _timeUseCase.isPause)
                .Subscribe(_ =>
                {
                    if (_inputUseCase.isDecision)
                    {
                        _seController.Play(SeType.Decision);
                        switch (_pauseView.type)
                        {
                            case PauseItemType.Continue:
                                break;
                            case PauseItemType.Retry:
                                _seController.Stop();
                                _sceneLoader.LoadFadeCurrent();
                                break;
                            case PauseItemType.Top:
                                _seController.Stop();
                                _sceneLoader.LoadFade(SceneName.Top);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        _timeUseCase.CancelPause();
                    }

                    var vertical = _inputUseCase.verticalDown;
                    if (vertical > 0)
                    {
                        _seController.Play(SeType.MoveCursor);
                        _pauseView.CursorUp();
                    }
                    else if (vertical < 0)
                    {
                        _seController.Play(SeType.MoveCursor);
                        _pauseView.CursorDown();
                    }
                })
                .AddTo(_pauseView);

            _timeUseCase.isStop
                .Subscribe(x =>
                {
                    if (x)
                    {
                        _pauseView.Show();
                    }
                    else
                    {
                        _pauseView.Hide();
                    }
                })
                .AddTo(_pauseView);
        }
    }
}