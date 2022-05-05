using System;
using EFUK;
using Soroeru.InGame.Data.Entity;
using Soroeru.InGame.Domain.Repository;
using Soroeru.InGame.Presentation.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class PlayerAttackUseCase
    {
        private readonly AttackRepository _attackRepository;
        private readonly Transform _transform;
        private EquipType _equipType;
        private BaseAttackCollision _attackCollision;

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

            if (data.time.EqualZero())
            {
                throw new Exception($"Attack life time is zero. (type: {type})");
            }

            if (data.power == 0)
            {
                throw new Exception($"Attack power is zero. (type: {type})");
            }

            if (_attackCollision != null)
            {
                var attackEntity = new AttackEntity(_transform, direction, data.time, data.power);
                _attackCollision.Fire(attackEntity);
            }
            else
            {
                var initPosition = _transform.position + (direction.ConvertVector3() * 0.3f);
                var collision = Object.Instantiate(data.collision, initPosition, Quaternion.identity);
                var attackEntity = new AttackEntity(_transform, direction, data.time, data.power);
                collision.Fire(attackEntity);
            }
        }

        public void EquipWeapon(EquipType type)
        {
            // 同じものを装備している場合
            if (_equipType == type) return;

            var data = _attackRepository.FindAttackData(type);
            if (data == null)
            {
                throw new Exception($"Can't find Attack data. (type: {type})");
            }

            if (data.collision == null)
            {
                throw new Exception($"Can't get Attack collision. (type: {type})");
            }

            if (data.time.EqualZero())
            {
                throw new Exception($"Attack life time is zero. (type: {type})");
            }

            if (data.power == 0)
            {
                throw new Exception($"Attack power is zero. (type: {type})");
            }

            _equipType = type;
            if (_attackCollision)
            {
                Object.Destroy(_attackCollision.gameObject);
            }

            if (data.isFirst)
            {
                _attackCollision = Object.Instantiate(data.collision, _transform.position, Quaternion.identity);
                var attackEntity = new AttackEntity(_transform, default, data.time, data.power);
                _attackCollision.Equip(attackEntity);
            }
            else
            {
                _attackCollision = null;
            }
        }
    }
}