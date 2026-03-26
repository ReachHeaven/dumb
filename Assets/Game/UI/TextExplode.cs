using System.Collections;
using Foundation.Events;
using Game.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace Game.UI
{
    public class TextExplode : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image pixelPrefab;
        [SerializeField] private Transform canvas;
        [SerializeField] private int pixelCount = 30;
        [SerializeField] private UnityEvent response;
        private bool _isClicked;

        public void Play()
        {
            if (!_isClicked)
            {
                _isClicked = true;
                StartCoroutine(ExplodeSequence());
            }
        }

        private IEnumerator ExplodeSequence()
        {
            yield return Shake(0.3f, 8f);
            text.gameObject.SetActive(false);
            SpawnPixels();
            yield return new WaitForSeconds(0.5f);
            response.Invoke();
            _isClicked = false;
        }

        private IEnumerator Shake(float duration, float intensity)
        {
            Vector3 original = text.rectTransform.anchoredPosition;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-intensity, intensity);
                float y = Random.Range(-intensity, intensity);
                text.rectTransform.anchoredPosition = original + new Vector3(x, y, 0);
                elapsed += Time.deltaTime;
                yield return null;
            }

            text.rectTransform.anchoredPosition = original;
        }

        private void SpawnPixels()
        {
            Vector3 center = text.rectTransform.position;
            float textWidth = text.rectTransform.rect.width;
            float textHeight = text.rectTransform.rect.height;

            for (int i = 0; i < pixelCount; i++)
            {
                Image pixel = Instantiate(pixelPrefab, canvas);

                Vector3 spawnPos = center + new Vector3(
                    Random.Range(-textWidth / 2f, textWidth / 2f),
                    Random.Range(-textHeight / 2f, textHeight / 2f),
                    0);

                pixel.rectTransform.position = spawnPos;
                pixel.rectTransform.sizeDelta = new Vector2(
                    Random.Range(3f, 10f), Random.Range(3f, 10f));
                pixel.color = text.color;

                StartCoroutine(ExplodePixel(pixel, center));
            }
        }

        private IEnumerator ExplodePixel(Image pixel, Vector3 center)
        {
            Vector2 direction = (pixel.rectTransform.position - center).normalized;
            if (direction == Vector2.zero)
                direction = Random.insideUnitCircle.normalized;

            direction = Quaternion.Euler(0, 0, Random.Range(-30f, 30f)) * direction;

            float speed = Random.Range(400f, 1200f);
            float rotationSpeed = Random.Range(-720f, 720f);
            float duration = Random.Range(0.4f, 0.8f);
            float elapsed = 0f;
            Color color = pixel.color;

            while (elapsed < duration)
            {
                float t = elapsed / duration;

                pixel.rectTransform.position += (Vector3)(direction * (speed * Time.deltaTime));
                speed *= 0.96f;

                pixel.rectTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

                if (t > 0.5f)
                {
                    color.a = 0f;
                    pixel.color = color;
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            Destroy(pixel.gameObject);
        }
    }
}
