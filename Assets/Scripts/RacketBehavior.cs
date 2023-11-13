using UnityEngine;

namespace Pong
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RacketBehavior : MonoBehaviour
    {
        // A value to keep the rackets in view
        private const float PlayerOffset = 1.5f;
        
        [Tooltip("The Paddle speed.")]
        [SerializeField] private float speed = 10f;

        [Tooltip("The Paddle side.")]
        [SerializeField] private PlayerSide side;

        [Header("Controls")]
        [SerializeField] private KeyCode up = KeyCode.UpArrow;
        [SerializeField] private KeyCode down = KeyCode.DownArrow;
        
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

        private void Start()
        {
            switch (side)
            {
                case PlayerSide.Left:
                    _rigidbody.position = Vector2.right * (CameraBounds.Left + PlayerOffset);
                    break;
                
                case PlayerSide.Right:
                    _rigidbody.position = Vector2.right * (CameraBounds.Right - PlayerOffset);
                    break;
            }
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
            // Cancel the direction in case no key is pressed
            _direction = 0;

            if (Input.GetKey(up))
            {
                _direction = 1;
            }
            else if (Input.GetKey(down))
            {
                _direction = -1;
            }
        }
    }
}