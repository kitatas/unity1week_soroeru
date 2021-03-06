using UnityEngine;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class PlayerAnimatorUseCase
    {
        private readonly Animator _animator;
        private static readonly int _speed = Animator.StringToHash("Speed");
        private static readonly int _fallSpeed = Animator.StringToHash("FallSpeed");
        private static readonly int _groundDistance = Animator.StringToHash("GroundDistance");
        private static readonly int _attack = Animator.StringToHash("Attack1");
        private static readonly int _damage = Animator.StringToHash("Damage");
        private static readonly int _isDead = Animator.StringToHash("IsDead");
        private static readonly int _clear = Animator.StringToHash("Clear");
        private static readonly int _finish = Animator.StringToHash("Finish");

        public PlayerAnimatorUseCase(Animator animator)
        {
            _animator = animator;
        }

        public void SetRun(float value)
        {
            var axisValue = Mathf.Abs(value);
            _animator.SetFloat(_speed, axisValue);
        }

        public void SetFall(float value)
        {
            _animator.SetFloat(_fallSpeed, value);
        }

        public void SetGround(bool value)
        {
            var distance = value ? 0.0f : 99.0f;
            _animator.SetFloat(_groundDistance, distance);
        }

        public void SetAttack()
        {
            _animator.SetTrigger(_attack);
        }

        public void SetDamage()
        {
            _animator.SetTrigger(_damage);
        }

        public void SetDead()
        {
            _animator.SetTrigger(_damage);
            _animator.SetBool(_isDead, true);
        }

        public void SetClear()
        {
            _animator.SetTrigger(_clear);
        }

        public void SetFinish()
        {
            _animator.SetTrigger(_finish);
        }
    }
}