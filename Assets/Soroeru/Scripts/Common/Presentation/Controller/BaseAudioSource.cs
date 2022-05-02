using UnityEngine;

namespace Soroeru.Common.Presentation.Controller
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseAudioSource : MonoBehaviour
    {
        private AudioSource _source;
        protected AudioSource audioSource => _source ??= GetComponent<AudioSource>();
    }
}