using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Soroeru.Common.Presentation.View;
using UnityEngine.SceneManagement;

namespace Soroeru.Common.Presentation.Controller
{
    public sealed class SceneLoader : IDisposable
    {
        private readonly TransitionMaskView _transitionMaskView;
        private readonly CancellationTokenSource _tokenSource;

        public SceneLoader(TransitionMaskView transitionMaskView)
        {
            _transitionMaskView = transitionMaskView;
            _tokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }

        public void LoadFade(SceneName sceneName)
        {
            LoadFadeAsync(sceneName, _tokenSource.Token).Forget();
        }

        private async UniTaskVoid LoadFadeAsync(SceneName sceneName, CancellationToken token)
        {
            await _transitionMaskView.FadeInAsync(token);
            await SceneManager.LoadSceneAsync(sceneName.ToString());
            await _transitionMaskView.FadeOutAsync(token);
        }
    }
}