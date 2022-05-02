using System;
using Soroeru.Common.Domain.Repository;
using UnityEngine;

namespace Soroeru.Common.Domain.UseCase
{
    public sealed class SoundUseCase : IBgmUseCase, ISeUseCase
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

        public AudioClip GetSe(SeType type)
        {
            var data = _soundRepository.FindSe(type);
            if (data == null)
            {
                throw new Exception("Can't find SE data.");
            }

            return data.clip;
        }
    }
}