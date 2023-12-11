using System;
using TMPro;
using UnityEngine;

namespace Pong
{
    public class ScoreZoneBehavior : MonoBehaviour
    {
        [Tooltip("The associated text component.")]
        [SerializeField] private TextMeshProUGUI scoreTextUI;
        
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ball"))
            {
                // Increment score
                Score++;
                
                // check the score
                GameManager.Instance.CheckScores();
            }
        }
    }
}