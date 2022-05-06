using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class MoveEnemyView : EnemyView
    {
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private float width = default;
        [SerializeField] private float offset = default;
        [SerializeField] private bool isHorizontal = default;
        [SerializeField] private bool isVertical = default;

        protected override void Tick()
        {
            var time = (Time.time + offset) * moveSpeed;
            var x = isHorizontal ? Mathf.Cos(time) * width : 0.0f;
            var y = isVertical ? Mathf.Sin(time) * width : 0.0f;
            transform.position = initPosition + new Vector3(x, y, 0.0f);
        }
    }
}