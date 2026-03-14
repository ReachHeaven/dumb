using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        public void UpdateScore(int value)
        {
            scoreText.text = $"Score: {value.ToString()}";
        }

        public void OnRestart()
        {
            scoreText.text = $"Score: 0";
        }
    }
}
