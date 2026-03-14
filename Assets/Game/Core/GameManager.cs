using Foundation;
using Foundation.Events;
using Game.UI;
using UnityEngine;

namespace Game.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private IntGameEvent onScoreChanged;
        [SerializeField] private GameEvent onGameOver;
        [SerializeField] private BoardView boardView;
        private readonly GameBoard _gameBoard = new();

        private void Start()
        {
            _gameBoard.SpawnRandomTile();
            _gameBoard.SpawnRandomTile();
            _gameBoard.SpawnRandomTile();
            _gameBoard.PrintBoard();
        }

        /// <summary>
        /// Swipe handler
        /// </summary>
        /// <param name="swipeDirection">Swipe direction</param>
        public void OnSwipe(Direction swipeDirection)
        {
            bool moved = _gameBoard.Move(swipeDirection);

            if (moved)
            {
                onScoreChanged.Raise(_gameBoard.Score);
                boardView.UpdateBoard(_gameBoard.GetFlatBoard());
            }

            if (_gameBoard.IsGameOver())
            {
                onGameOver.Raise();
                Debug.Log("GAME OVER! Score: " + _gameBoard.Score);
            }
        }
    }
}
