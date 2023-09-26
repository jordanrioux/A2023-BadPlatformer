using Platformer.Behaviors;
using UnityEngine;

namespace Platformer.Helpers
{
    public class FlipAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _sprite;

        private IMove _mover;

        private void Start()
        {
            _mover = GetComponent<IMove>();
        }

        private void Update()
        {
            if (_mover.Speed != 0f)
            {
                Flip();
            }
        }

        private void Flip()
        {
            var localScale = _sprite.localScale;
            localScale.x = _mover.Direction;
            _sprite.localScale = localScale;
        }
    }
}