using Foundation;
using UnityEngine;

namespace Game.Core
{
    public class GameManager : MonoBehaviour
    {
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
            _gameBoard.Move(swipeDirection);
            _gameBoard.PrintBoard();
        }
    }
}
