using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "BoardData", menuName = "Game/BoardData", order = 0)]
    public class BoardData : ScriptableObject
    {
        public int[] Board { get; private set; }

        public void UpdateBoard(int[] board)
        {
            Board = board;
        }
    }
}
