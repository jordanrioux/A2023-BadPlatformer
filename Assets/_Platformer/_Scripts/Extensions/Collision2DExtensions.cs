using Platformer.Player;
using UnityEngine;

namespace Extensions
{
    public static class Collision2DExtensions
    {
        public static bool WasHitByPlayer(this Collision2D collision)
        {
            return collision.collider.TryGetComponent<PlayerController>(out _);
        }
    
        public static bool WasBottom(this Collision2D collision)
        {
            return collision.contacts[0].normal.y > 0.5;
        }
        
        public static bool WasTop(this Collision2D collision)
        {
            return collision.contacts[0].normal.y < -0.5;
        }
    }
}