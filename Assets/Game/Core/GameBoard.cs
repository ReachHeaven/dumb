using System;
using System.Collections.Generic;

namespace Game.Core
{
    public class GameBoard
    {
        private int [,] _cells = new int[4,4];

        private bool HasEmptyCell()
        {
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                for (int j = 0; j < _cells.GetLength(1); j++)
                {
                    if (_cells[i, j] == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private List<(int row, int col)> GetEmptyCells()
        {
            var emptyCells = new List<(int row, int col)>();

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

        public void SpawnRandomTile()
        {
            List<(int row, int col)> emptyCells = GetEmptyCells();

            if (emptyCells.Count == 0)
            {
                return;
            }

            var random = new Random();
            (int row, int col) cell = emptyCells[random.Next(emptyCells.Count)];
            int value = random.Next(10) == 0 ? 4 : 2;

            _cells[cell.row, cell.col] = value;
        }

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

            UnityEngine.Debug.Log(output);
        }
    }
}
