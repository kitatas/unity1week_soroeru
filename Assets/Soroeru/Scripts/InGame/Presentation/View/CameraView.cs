using Cinemachine;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class CameraView : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera = default;

        private CinemachineFramingTransposer _framingTransposer;

        public void Init()
        {
            _framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }

        public void Tick(Direction direction)
        {
            _framingTransposer.m_ScreenX = direction == Direction.Right ? 0.4f : 0.6f;
        }
    }
}