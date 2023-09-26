using Platformer.Player;
using UnityEngine;

namespace Platformer.Checkpoint
{
    public class Checkpoint : MonoBehaviour
    {
        public bool Passed { get; private set; }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<PlayerController>(out _))
            {
                Passed = true;
            }
        }
    }
}