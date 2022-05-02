using UnityEngine;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class PlayerAnimatorUseCase
    {
        private readonly Animator _animator;
        private static readonly int _isRun = Animator.StringToHash("IsRun");

        public PlayerAnimatorUseCase(Animator animator)
        {
            _animator = animator;
        }

        public void SetRun(bool value)
        {
            _animator.SetBool(_isRun, value);
        }
    }
}