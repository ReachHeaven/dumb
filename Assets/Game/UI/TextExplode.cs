using System.Collections;
using Foundation.Events;
using Game.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.UI
{
    public class TextExplode : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Image pixelPrefab;
        [SerializeField] private Transform canvas;
        [SerializeField] private int pixelCount = 30;

        public void Play()
        {
            StartCoroutine(ExplodeSequence());
        }

        private IEnumerator ExplodeSequence()
        {
            yield return Shake(0.3f, 8f);
            text.gameObject.SetActive(false);
            SpawnPixels();
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
            // Направление от центра наружу
            Vector2 direction = (pixel.rectTransform.position - center).normalized;
            if (direction == Vector2.zero)
                direction = Random.insideUnitCircle.normalized;

            // Случайный разброс от основного направления
            direction = Quaternion.Euler(0, 0, Random.Range(-30f, 30f)) * direction;

            float speed = Random.Range(400f, 1200f);
            float rotationSpeed = Random.Range(-720f, 720f);
            float duration = Random.Range(0.4f, 0.8f);
            float elapsed = 0f;
            Color color = pixel.color;

            while (elapsed < duration)
            {
                float t = elapsed / duration;

                // Движение с замедлением
                pixel.rectTransform.position += (Vector3)(direction * speed * Time.deltaTime);
                speed *= 0.96f;

                // Вращение
                pixel.rectTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

                // Исчезновение в конце
                if (t > 0.5f)
                {
                    color.a = 1f - (t - 0.5f) * 2f;
                    pixel.color = color;
                }

                elapsed += Time.deltaTime;
                yield return null;
            }

            Destroy(pixel.gameObject);
        }
    }
}
