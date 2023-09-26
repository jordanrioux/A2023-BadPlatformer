using UnityEngine;

namespace Platformer.Player
{
    public class CharacterGrounding : MonoBehaviour
    {
        [Header("Raycast Settings")] [SerializeField]
        private float _maxDistance = 0.02f;

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform[] _groundChecks;

        private readonly RaycastHit2D[] _raycastResults = new RaycastHit2D[4];

        public bool IsGrounded { get; set; }

        private void FixedUpdate()
        {
            foreach (var pos in _groundChecks)
            {
                IsGrounded = CheckCharacterGrounding(pos);
                if (IsGrounded) break;
            }
        }

        private bool CheckCharacterGrounding(Transform transform)
        {
            var raycastHitCount = Physics2D.RaycastNonAlloc(transform.position, transform.forward, _raycastResults,
                _maxDistance, _layerMask);
            Debug.DrawRay(transform.position, transform.forward * _maxDistance, Color.red);
            return raycastHitCount != 0;
        }
    }
}


