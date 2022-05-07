using System;

namespace Soroeru.Common
{
    public static class CustomExtension
    {
        public static SceneName NextScene(this SceneName sceneName)
        {
            switch (sceneName)
            {
                case SceneName.Top:
                    return SceneName.Main1;
                case SceneName.Main1:
                    return SceneName.Main2;
                case SceneName.Main2:
                    return SceneName.Main3;
                case SceneName.Main3:
                    return SceneName.Top;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sceneName), sceneName, null);
            }
        }
    }
}