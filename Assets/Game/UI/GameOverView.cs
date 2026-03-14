using Game.Core;
using UnityEngine;

namespace Game.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverPanel;

        public void OnGameOver()
        {
            gameOverPanel.SetActive(true);
        }

        public void OnRestart()
        {
            gameOverPanel.SetActive(false);
        }
    }
}
