using UnityEngine;

namespace Soroeru.Common.Domain.UseCase
{
    public interface IBgmUseCase
    {
        AudioClip GetBgm(BgmType type);
    }
}