using System;
using Soroeru.Common.Domain.Repository;
using UnityEngine;

namespace Soroeru.Common.Domain.UseCase
{
    public sealed class SoundUseCase : IBgmUseCase
    {
        private readonly SoundRepository _soundRepository;

        public SoundUseCase(SoundRepository soundRepository)
        {
            _soundRepository = soundRepository;
        }

        public AudioClip GetBgm(BgmType type)
        {
            var data = _soundRepository.FindBgm(type);
            if (data == null)
            {
                throw new Exception("Can't find BGM data.");
            }

            return data.clip;
        }
    }
}