using Platformer.Managers;
using UnityEngine;

namespace Platformer.Checkpoint
{
    public class CompleteLevelFlag : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            GameManager.Instance.MoveToNextLevel();
        }
    }
}
