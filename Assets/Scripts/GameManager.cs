using TMPro;
using UnityEngine;

namespace Pong
{
    public class GameManager : MonoBehaviour
    {
        private const string WinnerText = "WINNER";
        private const string LoserText = "LOSER";
        
        // The game should stop when this is reached.
        private const int MaxScore = 11;

        [Header("Score Zones")]
        [SerializeField] private ScoreZoneBehavior leftPlayerZone;
        [SerializeField] private ScoreZoneBehavior rightPlayerZone;

        [Header("Score UI")]
        [SerializeField] private TextMeshProUGUI leftScoreUI;
        [SerializeField] private TextMeshProUGUI rightScoreUI;

        [Header("Ball and Rackets")]
        [SerializeField] private BallBehavior ball;

        public void CheckScores()
        {
            // if no player meets the max score
            if (leftPlayerZone.Score < MaxScore && rightPlayerZone.Score < MaxScore)
            {
                // reset the ball
                ball.Launch();
                
                // stop the code here
                return;
            }
            
            // End the game
            
            // if left player wins
            if (leftPlayerZone.Score >= MaxScore)
            {
                // change text to show left player won
                leftScoreUI.text = WinnerText;
                rightScoreUI.text = LoserText;
            }
            // else if right player wins
            else if (rightPlayerZone.Score >= MaxScore)
            {
                // change text to show right player won
                leftScoreUI.text = LoserText;
                rightScoreUI.text = WinnerText;
            }
            
            // Reset the game after a few seconds
        }
    }
}