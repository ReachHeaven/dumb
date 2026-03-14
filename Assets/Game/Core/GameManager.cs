using Foundation;
using Foundation.Events;
using Game.Data;
using UnityEngine;

namespace Game.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private IntGameEvent onScoreChanged;
        [SerializeField] private GameEvent onGameOver;
        [SerializeField] private GameEvent onRestart;
        [SerializeField] private BoardData boardData;
        [SerializeField] private GameEvent onBoardChanged;
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
                boardData.UpdateBoard(_gameBoard.GetFlatBoard());
                onBoardChanged.Raise();
            }

            if (_gameBoard.IsGameOver())
            {
                onGameOver.Raise();
                Debug.Log("GAME OVER! Score: " + _gameBoard.Score);
            }
        }

        /// <summary>
        /// Restart game method
        /// </summary>
        public void Restart()
        {
            _gameBoard.Reset();
            onRestart.Raise();
            boardData.UpdateBoard(_gameBoard.GetFlatBoard());
            onBoardChanged.Raise();
        }
    }
}
