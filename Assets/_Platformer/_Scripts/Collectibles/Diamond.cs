using Platformer.Managers;
using UnityEngine;

namespace Platformer.Collectibles
{
    public class Diamond : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            GameManager.Instance.AddDiamond();
            Destroy(gameObject);
        }
    }
}
