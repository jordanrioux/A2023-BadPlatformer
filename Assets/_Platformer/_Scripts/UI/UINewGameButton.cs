using Platformer.Managers;
using UnityEngine;

namespace Platformer.UI
{
    public class UINewGameButton : MonoBehaviour
    {
        public void NewGame()
        {
            GameManager.Instance.MoveToNextLevel();
        }
    }
}
