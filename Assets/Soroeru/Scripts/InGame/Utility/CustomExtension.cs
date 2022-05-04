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

        public static Vector2 ConvertVector(this Direction direction)
        {
            return direction switch
            {
                Direction.Left  => Vector2.left,
                Direction.Right => Vector2.right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static EquipType ConvertForEquip(this PictureType type)
        {
            switch (type)
            {
                case PictureType.Gun:
                    return EquipType.Gun;
                case PictureType.Trump:
                    return EquipType.Trump;
                case PictureType.Puni:
                    return EquipType.None;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}