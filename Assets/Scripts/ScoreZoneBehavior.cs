using UnityEngine;

namespace Pong
{
    public class ScoreZoneBehavior : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ball"))
            {
                // Increment score
                
                // Reset ball
                BallBehavior ball = other.GetComponent<BallBehavior>();
                if (ball != null)
                {
                    ball.Launch();
                }
            }
        }
    }
}