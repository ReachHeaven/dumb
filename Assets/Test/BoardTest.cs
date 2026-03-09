using Game.Core;
using UnityEngine;

namespace Test
{
    public class BoardTest : MonoBehaviour
    {
        private void Start()
        {
            var board = new GameBoard();
            board.SpawnRandomTile();
            board.SpawnRandomTile();
            board.SpawnRandomTile();
            board.PrintBoard();
        }
    }
}
