using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class PigView : EnemyView
    {
        [SerializeField] private float moveSpeed = default;
        [SerializeField] private float jumpPower = default;
        [SerializeField] private ContactFilter2D filter2d = default;

        private Direction _direction;
        private Rigidbody2D _rigidbody2d;
        private SpriteRenderer _spriteRenderer;

        protected override void Start()
        {
            base.Start();

            _direction = Direction.Left;
            _rigidbody2d = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            this.OnCollisionEnter2DAsObservable()
                .Select(other => other.collider)
                .Subscribe(other =>
                {
                    if (_rigidbody2d.IsTouching(filter2d))
                    {
                        _rigidbody2d.velocity = jumpPower * Vector2.up
                                                + new Vector2(_rigidbody2d.velocity.x, 0.0f);
                        return;
                    }

                    if (_direction == Direction.Right)
                    {
                        _direction = Direction.Left;
                        _spriteRenderer.flipX = false;
                    }
                    else
                    {
                        _direction = Direction.Right;
                        _spriteRenderer.flipX = true;
                    }
                })
                .AddTo(this);
        }

        protected override void Tick()
        {
            _rigidbody2d.velocity = moveSpeed * _direction.ConvertVector2()
                                    + new Vector2(0.0f, _rigidbody2d.velocity.y);
        }
    }
}