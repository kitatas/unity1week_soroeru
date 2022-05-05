using System;
using System.Collections.Generic;
using Soroeru.InGame.Domain.Repository;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class BuffUseCase
    {
        private readonly BuffRepository _buffRepository;
        private readonly Dictionary<BuffType, Action<int>> _buffMap;

        public BuffUseCase(BuffRepository buffRepository)
        {
            _buffRepository = buffRepository;
            _buffMap = new Dictionary<BuffType, Action<int>>();
        }

        public void Push(BuffType type, Action<int> action)
        {
            _buffMap.Add(type, action);
        }

        public void SetUp(BuffType type)
        {
            var data = _buffRepository.FindBuffData(type);
            if (data == null)
            {
                throw new Exception($"Can't find Buff data. (type: {type})");
            }

            if (_buffMap.TryGetValue(type, out var action))
            {
                action?.Invoke(data.value);
            }
            else
            {
                throw new Exception($"Can't find Buff map. (type: {type})");
            }
        }

        public void SetUp(PictureType type)
        {
            var buffType = type.ConvertForBuff();
            if (buffType == BuffType.None)
            {
                return;
            }

            SetUp(buffType);
        }
    }
}