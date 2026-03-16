using System;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class MenuView : MonoBehaviour
    {
        [SerializeField] private GameObject menuView;
        [SerializeField] private TMP_Text bestScoreText;

        public void Start()
        {
            bestScoreText.text = $"Best Score: {PlayerPrefs.GetInt("BestScore")}";
        }

        public void Show()
        {
            bestScoreText.text = $"Best Score: {PlayerPrefs.GetInt("BestScore")}";
            menuView.SetActive(true);
        }

        public void Hide()
        {
            menuView.SetActive(false);
        }
    }
}
