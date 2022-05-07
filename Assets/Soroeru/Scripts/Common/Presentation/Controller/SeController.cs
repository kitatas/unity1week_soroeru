using Soroeru.Common.Domain.UseCase;
using VContainer;

namespace Soroeru.Common.Presentation.Controller
{
    public sealed class SeController : BaseAudioSource
    {
        private ISeUseCase _seUseCase;

        [Inject]
        private void Construct(ISeUseCase bgmUseCase)
        {
            _seUseCase = bgmUseCase;
        }

        public void Play(SeType type)
        {
            var clip = _seUseCase.GetSe(type);
            audioSource.PlayOneShot(clip);
        }

        public void PlayLoop(SeType type, bool isForce = false)
        {
            if (isForce == false && audioSource.isPlaying)
            {
                var clip = _seUseCase.GetSe(type);
                if (clip == audioSource.clip)
                {
                    return;
                }

                if (type == SeType.ReelRoll)
                {
                    return;
                }
            }

            audioSource.clip = _seUseCase.GetSe(type);
            audioSource.Play();
        }

        public void Stop(SeType type)
        {
            var clip = _seUseCase.GetSe(type);
            if (clip == audioSource.clip)
            {
                audioSource.Stop();
            }
        }
    }
}