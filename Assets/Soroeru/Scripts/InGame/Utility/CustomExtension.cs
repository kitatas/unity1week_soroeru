using System;
using UnityEngine;

namespace Soroeru.InGame
{
    public static class CustomExtension
    {
        public static Vector3 ConvertVector3(this Direction direction)
        {
            return direction switch
            {
                Direction.Left  => Vector3.left,
                Direction.Right => Vector3.right,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static Vector2 ConvertVector2(this Direction direction)
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
                case PictureType.None:
                    return EquipType.None;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}