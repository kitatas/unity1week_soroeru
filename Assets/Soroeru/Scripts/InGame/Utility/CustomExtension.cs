using System;
using UnityEngine;

namespace Soroeru.InGame
{
    public static class CustomExtension
    {
        public static Vector3 ConvertOffset(this Direction direction)
        {
            return direction switch
            {
                Direction.Left  => new Vector3(-0.3f, 0.0f, 0.0f),
                Direction.Right => new Vector3(0.3f, 0.0f, 0.0f),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static EquipType ConvertForEquip(this PictureType type)
        {
            switch (type)
            {
                case PictureType.Gun:
                    return EquipType.Gun;
                case PictureType.Flag:
                case PictureType.Puni:
                    return EquipType.None;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}