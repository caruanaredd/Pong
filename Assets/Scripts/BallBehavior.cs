using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pong
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BallBehavior : MonoBehaviour
    {
        [Tooltip("The ball speed.")]
        [SerializeField] private float speed = 10f;
        
        // The direction of movement.
        private Vector2 _direction = Vector2.zero;
        
        // The Rigidbody component moving this object.
        private Rigidbody2D _rigidbody;

        /// <summary>
        /// Stores attached components.
        /// </summary>
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        private void Start()
        {
            Launch();
        }

        /// <summary>
        /// Launches the ball. 
        /// </summary>
        public void Launch()
        {
            _rigidbody.MovePosition(Vector2.zero);

            float axis = Mathf.Sign(Random.Range(-1, 1));
            _direction = new Vector2(axis, 0);
            
            _rigidbody.velocity = _direction * speed;
        }
    }
}