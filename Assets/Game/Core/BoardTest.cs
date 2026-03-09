using System;
using UnityEngine;

namespace Game.Core
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
