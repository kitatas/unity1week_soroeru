namespace Soroeru.Common.Presentation.Controller
{
    public interface IVolumeController
    {
        float volume { get; }
        void SetVolume(float value);
    }
}