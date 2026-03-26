using System.Collections;
using Foundation;
using Foundation.Events;
using Game.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Core
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private IntGameEvent onScoreChanged;
        [SerializeField] private GameEvent onBestScoreChanged;
        [SerializeField] private GameEvent onGameOver;
        [SerializeField] private GameEvent onRestart;
        [SerializeField] private BoardData boardData;
        [SerializeField] private GameEvent onBoardChanged;
        [SerializeField] private GameEvent onGameStarted;
        private readonly GameBoard _gameBoard = new();
        private int _bestScore;

#if UNITY_EDITOR
        private void Update()
        {
            if (Keyboard.current.f1Key.wasPressedThisFrame)
            {
                _gameBoard.DebugFillAll();
                boardData.UpdateBoard(_gameBoard.GetFlatBoard());
                onBoardChanged.Raise();
            }
        }
#endif

        private void Start()
        {
            _bestScore = PlayerPrefs.GetInt(PrefsKeys.BestScore, 0);
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

                if (_gameBoard.Score > _bestScore)
                {
                    _bestScore = _gameBoard.Score;
                    PlayerPrefs.SetInt(PrefsKeys.BestScore, _bestScore);
                    onBestScoreChanged.Raise();
                }

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
        ///Start game from menu
        /// </summary>
        public void OnGameStarted()
        {
            _gameBoard.Reset();
            _gameBoard.SpawnRandomTile();
            _gameBoard.SpawnRandomTile();
            onGameStarted.Raise();
            boardData.UpdateBoard(_gameBoard.GetFlatBoard());
            onBoardChanged.Raise();
            onScoreChanged.Raise(0);
        }

        /// <summary>
        /// Restart game method
        /// </summary>
        public void Restart()
        {
            _gameBoard.Reset();
            _gameBoard.SpawnRandomTile();
            _gameBoard.SpawnRandomTile();
            onRestart.Raise();
            boardData.UpdateBoard(_gameBoard.GetFlatBoard());
            onBoardChanged.Raise();
            onScoreChanged.Raise(0);
        }
    }
}
