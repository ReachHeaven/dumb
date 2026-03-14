using Game.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.UI
{
    public class BoardView : MonoBehaviour
    {
        [FormerlySerializedAs("_cells")] [SerializeField] private CellView[] cells;
        [FormerlySerializedAs("_boardData")] [SerializeField] private BoardData boardData;

        /// <summary>
        /// UpdateBoard by BoardData
        /// </summary>
        public void UpdateBoard()
        {
            int[] board = boardData.Board;
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].SetValue(board[i]);
            }
        }
    }
}
