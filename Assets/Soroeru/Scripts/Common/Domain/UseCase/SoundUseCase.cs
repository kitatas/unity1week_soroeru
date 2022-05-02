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
                throw new Exception($"Can't find Bgm data. (type: {type})");
            }

            if (data.clip == null)
            {
                throw new Exception($"Can't get Bgm clip. (type: {type})");
            }

            return data.clip;
        }

        public AudioClip GetSe(SeType type)
        {
            var data = _soundRepository.FindSe(type);
            if (data == null)
            {
                throw new Exception($"Can't find Se data. (type: {type})");
            }

            if (data.clip == null)
            {
                throw new Exception($"Can't get Se clip. (type: {type})");
            }

            return data.clip;
        }
    }
}