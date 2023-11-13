using System;
using UnityEngine;

namespace Pong
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RacketBehavior : MonoBehaviour
    {
        [Tooltip("The Paddle speed.")]
        [SerializeField] private float speed = 10f;

        private float _direction = 0;
        
        // The Rigidbody component (to move this object)
        private Rigidbody2D _rigidbody;

        /// <summary>
        /// Stores attached components.
        /// </summary>
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Physics update.
        /// </summary>
        private void FixedUpdate()
        {
            Vector2 updatePos = _rigidbody.position + Vector2.up * (_direction * speed * Time.deltaTime);
            
            // Limit the Y position of the racket.
            updatePos.y = Mathf.Clamp(updatePos.y, -7.5f, 7.5f);
            
            _rigidbody.MovePosition(updatePos);
        }

        /// <summary>
        /// Player controls.
        /// </summary>
        private void Update()
        {
            _direction = Input.GetAxisRaw("Vertical");
        }
    }
}