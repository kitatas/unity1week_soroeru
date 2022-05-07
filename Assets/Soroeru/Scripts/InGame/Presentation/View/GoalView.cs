using System;
using EFUK;
using Soroeru.Common;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class GoalView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI clearText = default;

        public void Play(Action<Vector3> action)
        {
            var coinDropPosition = transform.position.SetY(3.0f);
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    action?.Invoke(coinDropPosition);
                })
                .AddTo(this);
        }

        public void DisplayClearText(SceneName sceneName)
        {
            switch (sceneName)
            {
                case SceneName.Main1:
                case SceneName.Main2:
                    clearText.text = "STAGE CLEAR!!";
                    break;
                case SceneName.Main3:
                    clearText.text = "CONGRATULATION!!";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sceneName), sceneName, null);
            }
        }
    }
}