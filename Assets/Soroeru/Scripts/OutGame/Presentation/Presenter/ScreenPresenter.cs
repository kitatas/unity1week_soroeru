using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Soroeru.OutGame.Domain.UseCase;
using Soroeru.OutGame.Presentation.Controller;
using UniRx;
using VContainer.Unity;

namespace Soroeru.OutGame.Presentation.Presenter
{
    public sealed class ScreenPresenter : IPostInitializable, IDisposable
    {
        private readonly ScreenUseCase _screenUseCase;
        private readonly ScreenController _screenController;
        private readonly CancellationTokenSource _tokenSource;

        public ScreenPresenter(ScreenUseCase screenUseCase, ScreenController screenController)
        {
            _screenUseCase = screenUseCase;
            _screenController = screenController;
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            _screenController.InitAsync(_tokenSource.Token).Forget();

            _screenUseCase.screenType
                .Subscribe(x =>
                {
                    ExecAsync(x, _tokenSource.Token).Forget();
                })
                .AddTo(_tokenSource.Token);
        }

        private async UniTaskVoid ExecAsync(ScreenType type, CancellationToken token)
        {
            var next = await _screenController.TickAsync(type, token);
            _screenUseCase.SetType(next);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}