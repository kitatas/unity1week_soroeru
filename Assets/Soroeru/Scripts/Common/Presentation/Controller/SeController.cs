using System;
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
            if (clip == null)
            {
                throw new Exception("Can't find BGM clip.");
            }

            audioSource.PlayOneShot(clip);
        }
    }
}