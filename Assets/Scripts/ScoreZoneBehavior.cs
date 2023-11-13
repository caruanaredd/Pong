using System;
using TMPro;
using UnityEngine;

namespace Pong
{
    public class ScoreZoneBehavior : MonoBehaviour
    {
        [Tooltip("The associated text component.")]
        [SerializeField] private TextMeshProUGUI scoreTextUI;
        
        // The active Game Manager
        private GameManager _gameManager;
        
        // The player's score.
        private int _score;

        /// <summary>
        /// The player's score.
        /// </summary>
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                scoreTextUI.text = _score.ToString();
            }
        }

        private void Awake()
        {
            // exactly like GetComponent, but much more expensive
            // do not use outside of Awake where possible!
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ball"))
            {
                // Increment score
                Score++;
                
                // check the score
                _gameManager.CheckScores();
            }
        }
    }
}