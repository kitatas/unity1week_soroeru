using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Soroeru.Common.Data.Entity;
using Soroeru.Common.Presentation.View;
using UnityEngine.SceneManagement;

namespace Soroeru.Common.Presentation.Controller
{
    public sealed class SceneLoader : IDisposable
    {
        private readonly SceneEntity _sceneEntity;
        private readonly TransitionMaskView _transitionMaskView;
        private readonly CancellationTokenSource _tokenSource;

        public SceneLoader(SceneEntity sceneEntity, TransitionMaskView transitionMaskView)
        {
            _sceneEntity = sceneEntity;
            _transitionMaskView = transitionMaskView;
            _tokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }

        public SceneName currentScene => _sceneEntity.value;

        public void LoadFade(SceneName sceneName)
        {
            LoadFadeAsync(sceneName, _tokenSource.Token).Forget();
        }

        public void LoadFadeCurrent()
        {
            LoadFadeAsync(currentScene, _tokenSource.Token).Forget();
        }

        public void LoadFadeNext()
        {
            LoadFadeAsync(currentScene.NextScene(), _tokenSource.Token).Forget();
        }

        private async UniTaskVoid LoadFadeAsync(SceneName sceneName, CancellationToken token)
        {
            await _transitionMaskView.FadeInAsync(token);
            _sceneEntity.Set(sceneName);
            await SceneManager.LoadSceneAsync(currentScene.ToString());
            await _transitionMaskView.FadeOutAsync(token);
        }
    }
}