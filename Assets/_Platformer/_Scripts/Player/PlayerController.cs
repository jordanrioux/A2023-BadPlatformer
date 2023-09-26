using System;
using System.Collections;
using Platformer.Behaviors;
using UnityEngine;

namespace Platformer.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IMove
    {
        [Header("Data")] [SerializeField] private float _defaultGravityScale = 1f;
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _jumpForce = 7.5f;
        [SerializeField] private float _jumpHangTime = 0.125f;

        [Header("Dependencies")] [SerializeField]
        private CharacterGrounding _characterGrounding;

        [SerializeField] private InputController _inputController;
        [SerializeField] private Rigidbody2D _rigidbody;

        public int Direction => _movementInput.x > 0 ? 1 : -1;
        public float Speed => _movementInput.x;

        public bool IsGrounded => _characterGrounding.IsGrounded;
        public bool IsJumping { get; set; }

        public bool HasPressedJumpButton => _inputController.HasPressedJumpButton;

        private Vector2 _movementInput;
        public Vector2 MovementInput => _movementInput;


        private void Awake()
        {
            // TODO: Remove and serialize the field from the Editor directly
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            SetGravityScale(_defaultGravityScale);
        }

        private void Update()
        {
            // TODO: Switch to new Input system instead of using the old one
            _movementInput.x = _inputController.X;
            _movementInput.y = _inputController.Y;

            // Check to see if we can jump and has pressed the "Jump" button
            // TODO: This is currently "frame perfect" (when ground check will be implemented), we should use jump buffering time for a grace period 
            if (CanJump() && HasPressedJumpButton)
            {
                Jump();
            }

            // If y velocity is negative while we were jumping, it means we've reach the jump apex and we are currently falling.
            if (IsJumping && _rigidbody.velocity.y < 0)
            {
                IsJumping = false;
            }

            // We are falling and we are holding down, we can increase the fall gravity to fall faster
            if (_rigidbody.velocity.y < 0 && _movementInput.y < 0)
            {
                SetGravityScale(_defaultGravityScale * 2.5f);

                // If wanted, cap the maximum fall speed to stop accelerating over large distance
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Max(_rigidbody.velocity.y, -10f));
            }
            else if (IsJumping && Mathf.Abs(_rigidbody.velocity.y) < _jumpHangTime)
            {
                // Reduce gravity when jumping for slower up time allowing for more precise control during jump
                SetGravityScale(_defaultGravityScale * 0.4f);
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Max(_rigidbody.velocity.y, -10f));
            }
            else if (_rigidbody.velocity.y < 0)
            {
                // Increase gravity while falling
                SetGravityScale(_defaultGravityScale * 1.75f);
            }
            else
            {
                SetGravityScale(_defaultGravityScale);
            }
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        // You can adjust the rigidbody gravity scale for better physics of the jump (slower up during jump, faster down during falling, etc.)
        private void SetGravityScale(float gravityScale)
        {
            _rigidbody.gravityScale = gravityScale;
        }

        private void HandleMovement()
        {
            var targetSpeed = _movementInput.x * _maxSpeed;
            targetSpeed = Mathf.Lerp(_rigidbody.velocity.x, targetSpeed, 1f);

            float accelerationRate;

            // You can adjust acceleration/deceleration amount while on Ground vs Air (to allow more or less air control, etc.).
            if (IsGrounded)
            {
                // If moving, it's acceleration otherwise it's deceleration
                accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? 2f : 1f;
            }
            else
            {
                accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? 1.75f : 1f;
            }

            var speedDifference = targetSpeed - _rigidbody.velocity.x;
            var movement = speedDifference * accelerationRate;

            _rigidbody.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }

        private void Jump()
        {
            IsJumping = true;
            _characterGrounding.IsGrounded = false;

            var force = _jumpForce;
            if (_rigidbody.velocity.y < 0)
            {
                force -= _rigidbody.velocity.y;
            }

            _rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }

        private bool CanJump()
        {
            return IsGrounded && !IsJumping;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("HIT");
        }
    }
}

