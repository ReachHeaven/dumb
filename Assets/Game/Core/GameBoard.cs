using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Game.Core
{
    public class GameBoard
    {
        private const int MaxCapacity = 16;
        private readonly  int [,] _cells = new int[4,4];
        private readonly Random _random = new();

        /// <summary>
        /// Get all empty cells
        /// </summary>
        /// <returns>List with empty cells coordinates</returns>
        private List<(int row, int col)> GetEmptyCells()
        {
            var emptyCells = new List<(int row, int col)>(MaxCapacity);

            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    if (_cells[i, j] == 0)
                    {
                        emptyCells.Add((i, j));
                    }
                }
            }
            return emptyCells;
        }

        /// <summary>
        /// Spawn random tile if can
        /// </summary>
        public void SpawnRandomTile()
        {
            List<(int row, int col)> emptyCells = GetEmptyCells();

            if (emptyCells.Count == 0)
            {
                return;
            }

            (int row, int col) cell = emptyCells[_random.Next(emptyCells.Count)];
            int value = _random.Next(10) == 0 ? 4 : 2;

            _cells[cell.row, cell.col] = value;
        }
        /// <summary>
        /// Debug method
        /// </summary>
        public void PrintBoard()
        {
            string output = "";

            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    output += _cells[i, j].ToString().PadLeft(5);
                }

                output += "\n";
            }

            Debug.Log(output);
        }
    }
}
