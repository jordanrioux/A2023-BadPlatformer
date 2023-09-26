using Platformer.Behaviors;
using UnityEngine;

namespace Platformer.Helpers
{
    public class Mover : MonoBehaviour, IMove
    {
        [SerializeField] private float _speed = 0.5f;
        
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;
        [SerializeField] private Transform _sprite;

        private float _positionPercent;

        public int Direction { get; private set; } = 1; 

        public float Speed => _speed;
        
        private void Update()
        {
            var distance = Vector3.Distance(_start.position, _end.position);
            var speedForDistance = _speed / distance;
            _positionPercent += Time.deltaTime * Direction * speedForDistance;

            _sprite.position = Vector3.Lerp(_start.position, _end.position, _positionPercent);    
                
            if (_positionPercent >= 1 && Direction == 1)
            {
                Direction = -1;
            }
            else if (_positionPercent < 0 && Direction == -1)
            {
                Direction = 1;
            }
        }
    }
}