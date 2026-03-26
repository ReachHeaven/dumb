using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class BreathAnimation : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private bool autostart;
        [SerializeField] private Color sourceColor = Color.white;
        [SerializeField] private Color targetColor = Color.goldenRod;

        private void OnEnable()
        {
            if (autostart)
            {
                StartCoroutine(ColorBreathe());
            }
        }

        public void StopPulse()
        {
            Debug.Log("StopPulse");
            StopAllCoroutines();
        }

        public void SetColor(Color color)
        {
            targetColor = color;
            StopAllCoroutines();
            StartCoroutine(ColorBreathe());
        }


        private IEnumerator ColorBreathe()
        {
            Color baseColor = sourceColor;
            Color finalColor = targetColor;
            finalColor.a = 1f;

            while (true)
            {
                yield return LerpColor(baseColor, finalColor);
                yield return LerpColor(finalColor, baseColor);
            }
        }

        private IEnumerator LerpColor(Color from, Color to, float duration = 0.8f)
        {
            float elapsed = 0f;

            while (elapsed < duration)
            {
                _image.color = Color.Lerp(from, to, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            _image.color = to;
        }
    }
}
