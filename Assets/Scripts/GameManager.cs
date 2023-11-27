using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pong
{
    [RequireComponent(typeof(PlayerInput))]
    public class GameManager : MonoBehaviour
    {
        private const string WinnerText = "WINNER";
        private const string LoserText = "LOSER";
        
        // The game should stop when this is reached.
        private const int MaxScore = 11;

        /// <summary>
        /// The global game state (ie. what screen we're on).
        /// </summary>
        public static GameState State { get; private set; } = GameState.Menu;

        [Header("Player Rackets")]
        [SerializeField] private RacketBehavior leftRacket;
        [SerializeField] private RacketBehavior rightRacket;

        [Header("Score Zones")]
        [SerializeField] private ScoreZoneBehavior leftPlayerZone;
        [SerializeField] private ScoreZoneBehavior rightPlayerZone;

        [Header("Score UI")]
        [SerializeField] private TextMeshProUGUI leftScoreUI;
        [SerializeField] private TextMeshProUGUI rightScoreUI;

        [Header("Ball and Rackets")]
        [SerializeField] private BallBehavior ball;

        [Header("Screens")]
        [SerializeField] private GameObject menuUI;
        [SerializeField] private GameObject gameplayUI;

        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            ChangeState(GameState.Menu);
        }

        private void ChangeState(GameState newState)
        {
            menuUI.SetActive(newState == GameState.Menu);
            gameplayUI.SetActive(newState != GameState.Menu);

            switch (newState)
            {
                case GameState.Menu:
                    _playerInput.SwitchCurrentActionMap("Menu");

                    break;

                case GameState.Playing:
                    _playerInput.SwitchCurrentActionMap("Gameplay");
                    
                    ball.Launch();

                    break;

                case GameState.Reset:

                    leftPlayerZone.Score = 0;
                    rightPlayerZone.Score = 0;

                    leftScoreUI.text = string.Empty;
                    rightScoreUI.text = string.Empty;

                    break;

                case GameState.EndGame:

                    // Stop the rackets from moving
                    leftRacket.SetDirection(0);
                    rightRacket.SetDirection(0);
                    
                    // Disable ball physics
                    ball.Stop();

                    // Reset the game
                    StartCoroutine(ResetGame());
                    
                    break;
            }

            State = newState;
        }

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
            ChangeState(GameState.EndGame);
        }

        private void OnLeftPlayer(InputValue value)
        {
            SetPlayerDirection(value, leftRacket);
        }

        private void OnRightPlayer(InputValue value)
        {
            SetPlayerDirection(value, rightRacket);
        }

        private void OnStart()
        {
            ChangeState(GameState.Playing);
        }

        private void SetPlayerDirection(InputValue value, RacketBehavior racket)
        {
            // Racket shouldn't move if not playing the game.
            if (State != GameState.Playing)
            {
                return;
            }

            int direction = (int)value.Get<float>();
            racket.SetDirection(direction);
        }

        /// <summary>
        /// Resets the game.
        /// </summary>
        private IEnumerator ResetGame()
        {
            yield return new WaitForSeconds(3f);

            ChangeState(GameState.Reset);
            ChangeState(GameState.Menu);
        }
    }
}