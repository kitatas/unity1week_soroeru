using Soroeru.Common.Domain.UseCase;
using VContainer;

namespace Soroeru.Common.Presentation.Controller
{
    public sealed class BgmController : BaseAudioSource
    {
        private IBgmUseCase _bgmUseCase;

        [Inject]
        private void Construct(IBgmUseCase bgmUseCase)
        {
            _bgmUseCase = bgmUseCase;
        }

        public void Play(BgmType type)
        {
            var clip = _bgmUseCase.GetBgm(type);
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void Stop()
        {
            audioSource.Stop();
        }
    }
}