using UnityEngine;

namespace Soroeru.Common.Presentation.Controller
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseAudioSource : MonoBehaviour, IVolumeController
    {
        private AudioSource _source;
        protected AudioSource audioSource => _source ??= GetComponent<AudioSource>();
        public float volume => audioSource.volume;

        public void SetVolume(float value)
        {
            audioSource.volume = value;
        }
    }
}