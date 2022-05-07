using System;
using Soroeru.Common;
using Soroeru.Common.Data.Entity;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class TweetUseCase
    {
        private readonly SceneEntity _sceneEntity;

        public TweetUseCase(SceneEntity sceneEntity)
        {
            _sceneEntity = sceneEntity;
        }

        public void Tweet(int score)
        {
            var tweetText = $"難易度:{GetSceneName(_sceneEntity.value)}をコイン{score}枚集めてクリアした！\n";
            tweetText += $"#{GameConfig.GAME_ID} #{GameConfig.HASH_TAG}";
            UnityRoomTweet.Tweet(GameConfig.GAME_ID, tweetText);
        }

        private static string GetSceneName(SceneName sceneName)
        {
            return sceneName switch
            {
                SceneName.Main1 => "Easy",
                SceneName.Main2 => "Normal",
                SceneName.Main3 => "Expert",
                _ => throw new ArgumentOutOfRangeException(nameof(sceneName), sceneName, null)
            };
        }
    }
}