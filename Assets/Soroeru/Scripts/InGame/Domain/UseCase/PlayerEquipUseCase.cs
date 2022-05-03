using System;
using EFUK;
using Soroeru.InGame.Data.DataStore;
using Soroeru.InGame.Domain.Repository;
using UniRx;
using UnityEngine;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class PlayerEquipUseCase
    {
        private readonly EquipRepository _equipRepository;
        private readonly ReactiveProperty<Sprite> _equipSprite;
        private readonly ReactiveProperty<float> _equipLifeTime;
        public EquipType currentEquip { get; private set; }

        public PlayerEquipUseCase(EquipRepository equipRepository)
        {
            _equipRepository = equipRepository;
            _equipSprite = new ReactiveProperty<Sprite>();
            _equipLifeTime = new ReactiveProperty<float>();
            Equip(EquipType.None);
        }

        public IReadOnlyReactiveProperty<Sprite> equipSprite => _equipSprite;
        public IReadOnlyReactiveProperty<float> equipLifeTime => _equipLifeTime;

        public void Equip(EquipType type)
        {
            var data = _equipRepository.FindEquipData(type);
            if (data == null)
            {
                throw new Exception($"Can't find Equip data. (type: {type})");
            }

            if (data.sprite == null)
            {
                throw new Exception($"Can't get Equip sprite. (type: {type})");
            }

            _equipSprite.Value = data.sprite;
            _equipLifeTime.Value = data.time;
            currentEquip = data.type;
        }

        public void Equip(PictureType type)
        {
            var equipType = type.ConvertForEquip();
            if (equipType == EquipType.None)
            {
                return;
            }

            Equip(equipType);
        }

        public void Tick(float deltaTime)
        {
            if (currentEquip == EquipType.None)
            {
                return;
            }

            _equipLifeTime.Value = Mathf.Max(_equipLifeTime.Value - deltaTime, 0.0f);
            if (_equipLifeTime.Value.EqualZero())
            {
                Equip(EquipType.None);
            }
        }
    }
}