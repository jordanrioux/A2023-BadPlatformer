using System.Linq;
using Platformer.Checkpoint;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Checkpoint[] _checkpoints;

    public Checkpoint GetLastCheckpointThatWasPassed()
    {
        return _checkpoints.LastOrDefault(checkpoint => checkpoint.Passed);
    }
}
