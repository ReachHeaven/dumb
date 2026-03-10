using System.Collections.Generic;
using Foundation;
using UnityEngine;
using Random = System.Random;

namespace Game.Core
{
    public class GameBoard
    {
        private const int MaxCapacity = 16;
        private const int GridSize = 4;
        private readonly int[,] _cells = new int[GridSize, GridSize];
        private int _score;
        private readonly Random _random = new();

        public int Score => _score;

        /// <summary>
        /// Handle move
        /// </summary>
        /// <param name="direction">Move direction</param>
        /// <returns>true if success</returns>
        public bool Move(Direction direction)
        {
            bool moved = false;

            for (int i = 0; i < GridSize; i++)
            {
                int[] line = GetLine(i, direction);
                int[] merged = MergeLine(line);

                if (!LinesEqual(line, merged))
                {
                    moved = true;
                }

                SetLine(i, direction, merged);
            }

            if (moved)
            {
                SpawnRandomTile();
            }

            return moved;
        }

        /// <summary>
        /// Has empty cell?
        /// </summary>
        /// <returns>Return true if have</returns>
        private bool HasEmptyCell()
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    if (_cells[i, j] == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check for empty cells
        /// and potential merge
        /// </summary>
        /// <returns>True if game over</returns>
        public bool IsGameOver()
        {
            if (HasEmptyCell())
            {
                return false;
            }

            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    int current = _cells[i, j];

                    if (j < GridSize - 1 && current == _cells[i, j + 1])
                    {
                        return false;
                    }

                    if (i < GridSize - 1 && current == _cells[i + 1, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private int[] GetLine(int index, Direction direction)
        {
            int[] line = new int[GridSize];

            for (int i = 0; i < line.Length; i++)
            {
                line[i] = direction switch
                {
                    Direction.Left => _cells[index, i],
                    Direction.Right => _cells[index, GridSize - 1 - i],
                    Direction.Up => _cells[i, index],
                    Direction.Down => _cells[GridSize - 1 - i, index],
                    _ => 0
                };
            }

            return line;
        }

        private void SetLine(int index, Direction direction, int[] line)
        {
            for (int i = 0; i < GridSize; i++)
            {
                switch (direction)
                {
                    case Direction.Left:
                        _cells[index, i] = line[i];
                        break;
                    case Direction.Right:
                        _cells[index, GridSize - 1 - i] = line[i];
                        break;
                    case Direction.Up:
                        _cells[i, index] = line[i];
                        break;
                    case Direction.Down:
                        _cells[GridSize - 1 - i, index] = line[i];
                        break;
                }
            }
        }

        private int[] MergeLine(int[] line)
        {
            int[] compressed = new int[GridSize];
            int position = 0;

            for (int i = 0; i < GridSize; i++)
            {
                if (line[i] != 0)
                {
                    compressed[position] = line[i];
                    position++;
                }
            }

            for (int i = 0; i < GridSize - 1; i++)
            {
                if (compressed[i] != 0 && compressed[i] == compressed[i + 1])
                {
                    compressed[i] *= 2;
                    _score += compressed[i];
                    compressed[i + 1] = 0;
                }
            }

            int[] result = new int[GridSize];
            position = 0;

            for (int i = 0; i < GridSize; i++)
            {
                if (compressed[i] != 0)
                {
                    result[position] = compressed[i];
                    position++;
                }
            }

            return result;
        }

        private bool LinesEqual(int[] a, int[] b)
        {
            for (int i = 0; i < GridSize; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }

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
