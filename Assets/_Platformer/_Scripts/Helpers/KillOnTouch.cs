using Extensions;
using Platformer.Managers;
using Platformer.Player;
using UnityEngine;

namespace Platformer.Helpers
{
    public class KillOnTouch : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.WasHitByPlayer())
            {
                GameManager.Instance.KillPlayer();
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<PlayerController>(out _))
            {
                GameManager.Instance.KillPlayer();
            }
        }
    }   
}
