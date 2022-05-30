using System;
using Cinemachine;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class CameraView : MonoBehaviour
    {
        [SerializeField] private Collider2D popRange = default;
        [SerializeField] private CinemachineVirtualCamera virtualCamera = default;

        private CinemachineFramingTransposer _framingTransposer;

        public void Init(Action<Collider2D> action)
        {
            _framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

            action?.Invoke(popRange);
        }

        public void Tick(Direction direction)
        {
            _framingTransposer.m_ScreenX = direction == Direction.Right ? 0.4f : 0.6f;
        }
    }
}