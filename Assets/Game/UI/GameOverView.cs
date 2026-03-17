using Game.Core;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private TMP_Text bestScoreText;

        public void OnGameOver()
        {
            bestScoreText.text = $"Best score: {PlayerPrefs.GetInt(PrefsKeys.BestScore, 0)}";
            gameOverPanel.SetActive(true);
        }

        public void Hide()
        {
            gameOverPanel.SetActive(false);
        }
    }
}
