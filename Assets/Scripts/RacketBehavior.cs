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
            _rigidbody.MovePosition(_rigidbody.position + Vector2.up * (_direction * speed * Time.deltaTime));
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