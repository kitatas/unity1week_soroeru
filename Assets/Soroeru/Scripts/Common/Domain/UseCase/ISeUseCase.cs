using UnityEngine;

namespace Soroeru.Common.Domain.UseCase
{
    public interface ISeUseCase
    {
        AudioClip GetSe(SeType type);
    }
}