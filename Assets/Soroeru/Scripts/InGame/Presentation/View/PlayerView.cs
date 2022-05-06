using System;
using EFUK;
using Soroeru.InGame.Data.Entity;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private ArmorAttackView armorAttackView = default;
        public ArmorAttackView armorView => armorAttackView;

        public Vector3 position => transform.position;

        public Direction direction { get; private set; } = Direction.Right;

        public Action<float> jumpAction { get; private set; }

        public void Init(Action<float> jump)
        {
            jumpAction = jump;
        }

        public void SetLayer(string layerName)
        {
            gameObject.SetLayer(layerName);
        }

        public void SetDirection(Direction dir)
        {
            direction = dir;
        }

        public Action PowerUp(float lifeTime)
        {
            SetLayer(LayerConfig.DAMAGED);
            var armor = Instantiate(armorView, position, Quaternion.identity);
            var attackEntity = new AttackEntity(transform, direction, lifeTime, 999);
            armor.Fire(attackEntity);

            return () =>
            {
                SetLayer(LayerConfig.PLAYER);
                Destroy(armor.gameObject);
            };
        }
    }
}