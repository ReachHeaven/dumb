using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class BreathAnimation : MonoBehaviour
    {
        private Image _image;

        public void Start()
        {
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            StartCoroutine(ColorBreathe());
        }

        private IEnumerator ColorBreathe()
        {
            Color baseColor = _image.color;
            Color finalColor = baseColor * 1.3f;
            finalColor.a = 1f;

            while (true)
            {
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

            elapsed = 0f;

            while (elapsed < duration)
            {
                _image.color = Color.Lerp(to, from, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            _image.color = to;
        }
    }
}
