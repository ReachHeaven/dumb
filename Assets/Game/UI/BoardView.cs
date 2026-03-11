using UnityEngine;

namespace Game.UI
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] CellView[] _cells;

        public void UpdateBoard(int[] flatBoard)
        {
            for (int i = 0; i < _cells.Length; i++)
            {
                _cells[i].SetValue(flatBoard[i]);
            }
        }
    }
}
