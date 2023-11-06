using UnityEngine;

namespace Pong
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BallBehavior : MonoBehaviour
    {
        [Tooltip("The ball speed.")]
        [SerializeField] private float speed = 10f;

        [Tooltip("The angle at which the ball will reflect off the racket.")]
        [SerializeField] private float maxReflectionAngle = 60f;
        
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

        private void OnCollisionEnter2D(Collision2D other)
        {
            // if the other object is the Player
            if (other.gameObject.CompareTag("Player"))
            {
                // Calculate the ball's position for reflection
                float hitFactor = HitFactor(
                    transform.position,
                    other.transform.position,
                    other.collider.bounds.size.y);

                // The speed on the Y axis depending on the angle
                float reflectionY = Mathf.Sin(hitFactor * Mathf.Deg2Rad * maxReflectionAngle);
                float reflectionX = Mathf.Sign(_rigidbody.velocity.x);
                
                // Set the new direction
                Vector2 newDirection = new Vector2(reflectionX, reflectionY).normalized;

                // Update the rigidbody velocity
                _rigidbody.velocity = newDirection * speed;
            }
        }

        /// <summary>
        /// Calculates the offset position between the ball and racket.
        /// </summary>
        /// <param name="ballPosition">The ball position.</param>
        /// <param name="racketPosition">The racket position.</param>
        /// <param name="racketHeight">The racket height.</param>
        /// <returns>A number between -1 and 1.</returns>
        private float HitFactor(Vector2 ballPosition, Vector2 racketPosition, float racketHeight)
        {
            return (ballPosition.y - racketPosition.y) / racketHeight;
        }
    }
}