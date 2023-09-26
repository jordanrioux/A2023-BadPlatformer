using UnityEngine;

namespace Platformer.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [Header("Dependencies")] [SerializeField]
        private Animator _animator;

        [SerializeField] private InputController _inputController;
        [SerializeField] private PlayerController _playerController;

        private void Update()
        {
            _animator.SetFloat("Speed", Mathf.Abs(_playerController.MovementInput.x));
            _animator.SetBool("Grounded", _playerController.IsGrounded);

            if (_inputController.HasPressedJumpButton)
            {
                _animator.SetTrigger("Jump");
            }

            if (_inputController.HasPressedAttackButton)
            {
                _animator.SetTrigger("Attack");
            }
        }
    }
}
