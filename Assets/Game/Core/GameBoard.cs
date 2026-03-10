using System.Collections.Generic;
using Foundation;
using UnityEngine;
using Random = System.Random;

namespace Game.Core
{
    public class GameBoard
    {
        private const int MaxCapacity = 16;
        private readonly int[,] _cells = new int[4, 4];
        private readonly Random _random = new();

        /// <summary>
        /// Handle move
        /// </summary>
        /// <param name="direction">Move direction</param>
        /// <returns>true if success</returns>
        public bool Move(Direction direction)
        {
            bool moved = false;

            for (int i = 0; i < 4; i++)
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

        private int[] GetLine(int index, Direction direction)
        {
            int[] line = new int[4];

            for (int i = 0; i < line.Length; i++)
            {
                line[i] = direction switch
                {
                    Direction.Left => _cells[index, i],
                    Direction.Right => _cells[index, 3 - i],
                    Direction.Up => _cells[i, index],
                    Direction.Down => _cells[3 - i, index],
                    _ => 0
                };
            }

            return line;
        }

        private void SetLine(int index, Direction direction, int[] line)
        {
            for (int i = 0; i < 4; i++)
            {
                switch (direction)
                {
                    case Direction.Left:
                        _cells[index, i] = line[i];
                        break;
                    case Direction.Right:
                        _cells[index, 3 - i] = line[i];
                        break;
                    case Direction.Up:
                        _cells[i, index] = line[i];
                        break;
                    case Direction.Down:
                        _cells[3 - i, index] = line[i];
                        break;
                }
            }
        }

        private int[] MergeLine(int[] line)
        {
            int[] compressed = new int[4];
            int position = 0;

            for (int i = 0; i < 4; i++)
            {
                if (line[i] != 0)
                {
                    compressed[position] = line[i];
                    position++;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (compressed[i] != 0 && compressed[i] == compressed[i + 1])
                {
                    compressed[i] *= 2;
                    compressed[i + 1] = 0;
                }
            }

            int[] result = new int[4];
            position = 0;

            for (int i = 0; i < 4; i++)
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
            for (int i = 0; i < 4; i++)
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
