using System;
using UniRx;
using UniRx.Triggers;
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
                case PictureType.None:
                case PictureType.Jump:
                case PictureType.Bomb:
                case PictureType.Skull:
                case PictureType.Seven:
                    return EquipType.None;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static ItemType ConvertForItem(this PictureType type)
        {
            switch (type)
            {
                case PictureType.Jump:
                    return ItemType.Jump;
                case PictureType.Bomb:
                    return ItemType.Bomb;
                case PictureType.None:
                case PictureType.Gun:
                case PictureType.Trump:
                case PictureType.Skull:
                case PictureType.Seven:
                    return ItemType.None;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static BuffType ConvertForBuff(this PictureType type)
        {
            switch (type)
            {
                case PictureType.Skull:
                    return BuffType.Skull;
                case PictureType.Seven:
                    return BuffType.Seven;
                case PictureType.None:
                case PictureType.Gun:
                case PictureType.Trump:
                case PictureType.Jump:
                case PictureType.Bomb:
                    return BuffType.None;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static void OnTriggerEnter<T>(this Component component, Action<T> action) where T : Component
        {
            component
                .OnTriggerEnter2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.TryGetComponent(out T t))
                    {
                        action?.Invoke(t);
                    }
                })
                .AddTo(component);
        }

        public static void OnTriggerExit<T>(this Component component, Action<T> action) where T : Component
        {
            component
                .OnTriggerExit2DAsObservable()
                .Subscribe(other =>
                {
                    if (other.TryGetComponent(out T t))
                    {
                        action?.Invoke(t);
                    }
                })
                .AddTo(component);
        }
    }
}