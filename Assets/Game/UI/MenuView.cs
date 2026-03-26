using Game.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private GameObject menuView;
        [SerializeField] private TMP_Text bestScoreText;
        [SerializeField] private Button startButton;

        public void Start()
        {
            bestScoreText.text = $"Best Score: {PlayerPrefs.GetInt(PrefsKeys.BestScore)}";
        }

        public void Show()
        {
            bestScoreText.text = $"Best Score: {PlayerPrefs.GetInt(PrefsKeys.BestScore)}";
            menuView.SetActive(true);
        }

        public void Hide()
        {
            menuView.SetActive(false);
        }
    }
}
