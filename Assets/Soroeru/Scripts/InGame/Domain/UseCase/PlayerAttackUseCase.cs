using System;
using Soroeru.InGame.Domain.Repository;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class PlayerAttackUseCase
    {
        private readonly AttackRepository _attackRepository;
        private readonly Transform _transform;

        public PlayerAttackUseCase(AttackRepository attackRepository, Transform transform)
        {
            _attackRepository = attackRepository;
            _transform = transform;
        }

        public void Attack(EquipType type, Direction direction)
        {
            var data = _attackRepository.FindAttackData(type);
            if (data == null)
            {
                throw new Exception($"Can't find Attack data. (type: {type})");
            }

            if (data.collision == null)
            {
                throw new Exception($"Can't get Attack collision. (type: {type})");
            }

            var initPosition = _transform.position + direction.ConvertOffset();
            var collision = Object.Instantiate(data.collision, initPosition, Quaternion.identity);
            collision.Fire(_transform, direction);
        }
    }
}