using System;
using Cinemachine;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class CameraView : MonoBehaviour
    {
        [SerializeField] private Collider2D popRange = default;
        [SerializeField] private CinemachineVirtualCamera virtualCamera = default;

        private CinemachineFramingTransposer _framingTransposer;

        public void Init(Func<(EnemyType, Vector3), EnemyView> pop)
        {
            _framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

            popRange
                .OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.TryGetComponent(out EnemyPopView popView))
                    {
                        var enemy = pop?.Invoke((popView.type, popView.position));
                        popView.SetInstance(enemy);
                    }
                })
                .AddTo(this);

            popRange
                .OnTriggerExit2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.TryGetComponent(out EnemyPopView popView))
                    {
                        popView.DestroyInstance();
                    }
                })
                .AddTo(this);
        }

        public void Tick(Direction direction)
        {
            _framingTransposer.m_ScreenX = direction == Direction.Right ? 0.4f : 0.6f;
        }
    }
}