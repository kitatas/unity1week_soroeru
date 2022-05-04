using System;
using Soroeru.InGame.Domain.Repository;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class SlotItemUseCase
    {
        private readonly SlotItemRepository _slotItemRepository;
        private readonly Transform _transform;

        public SlotItemUseCase(SlotItemRepository slotItemRepository, Transform transform)
        {
            _slotItemRepository = slotItemRepository;
            _transform = transform;
        }

        public void Generate(ItemType type, Direction direction)
        {
            var data = _slotItemRepository.FindSlotItemData(type);
            if (data == null)
            {
                throw new Exception($"Can't find Slot item data. (type: {type})");
            }

            if (data.item == null)
            {
                throw new Exception($"Can't get Slot item. (type: {type})");
            }

            var initPosition = _transform.position
                               + direction.ConvertVector3() * 0.5f + Vector3.down * 0.1f;
            var item = Object.Instantiate(data.item, initPosition, Quaternion.identity);
            item.Generate(data.time);
        }

        public void Generate(PictureType type, Direction direction)
        {
            var itemType = type.ConvertForItem();
            if (itemType == ItemType.None)
            {
                return;
            }

            Generate(itemType, direction);
        }
    }
}