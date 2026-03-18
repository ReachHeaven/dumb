using System.Collections;
using Game.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.UI
{
    public class BoardView : MonoBehaviour
    {
        [FormerlySerializedAs("_cells")] [SerializeField]
        private CellView[] cells;

        [FormerlySerializedAs("_boardData")] [SerializeField]
        private BoardData boardData;

        [SerializeField] private TMP_Text popupPrefab;
        [SerializeField] private Canvas canvas;
        private int[] _previousBoard = new int[16];

        /// <summary>
        /// UpdateBoard by BoardData
        /// </summary>
        public void UpdateBoard()
        {
            int[] board = boardData.Board;
            for (int i = 0; i < cells.Length; i++)
            {
                int oldValue = _previousBoard[i];
                int newValue = board[i];

                cells[i].SetValue(board[i]);

                if (newValue != 0 && oldValue == 0)
                {
                    cells[i].PlayAppearAnimation();
                }

                else if (newValue != 0 && newValue > oldValue)
                {
                    cells[i].PlayMergeAnimation();
                    ShowScorePopup(newValue, cells[i].transform.position);
                }
            }

            board.CopyTo(_previousBoard, 0);
        }


        private void ShowScorePopup(int value, Vector3 position)
        {
            TMP_Text popup = Instantiate(popupPrefab, position, Quaternion.identity, canvas.transform);
            popup.text = $"+{value}";
            StartCoroutine(TextAnimation(popup));
        }

        private IEnumerator TextAnimation(TMP_Text text)
        {
            float duration = 0.5f;
            float elapsed = 0f;

            Vector3 start = text.transform.position;
            Color color = text.color;

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                text.transform.position = start + Vector3.up * (t * 50f);
                color.a = 1f - t;
                text.color = color;
                elapsed += Time.deltaTime;
                yield return null;
            }

            Destroy(text.gameObject);
        }
    }
}
