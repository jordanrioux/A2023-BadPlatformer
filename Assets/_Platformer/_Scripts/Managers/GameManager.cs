using System;
using Platformer.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public int Diamonds { get; private set; } 
        
        public int Lives { get; private set; }
        public int CurrentLevel { get; set; }
        
        public event Action<int> OnLivesChanged;
        public event Action<int> OnDiamondsChanged;
            
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                RestartGame();
            }
        }

        public void KillPlayer()
        {
            Lives--;
            OnLivesChanged?.Invoke(Lives);

            if (Lives <= 0)
            {
                RestartGame();
            }
            else
            {
                SendPlayerToCheckpoint();
            }
        }

        private void SendPlayerToCheckpoint()
        {
            var checkpointManager = FindObjectOfType<CheckpointManager>();
            var checkpoint = checkpointManager.GetLastCheckpointThatWasPassed();
            var player = FindObjectOfType<PlayerController>();
            player.transform.position = checkpoint.transform.position;
        }

        public void MoveToNextLevel()
        {
            CurrentLevel++;
            SceneManager.LoadScene(CurrentLevel);
        }
        
        private void RestartGame()
        {
            CurrentLevel = 0;
            
            Diamonds = 0;
            Lives = 3;
            SceneManager.LoadScene(CurrentLevel);
        }

        public void AddDiamond()
        {
            Diamonds++;
            OnDiamondsChanged?.Invoke(Diamonds);
        }
    }
}
