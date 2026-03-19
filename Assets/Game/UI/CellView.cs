using System.Collections;
using Game.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.UI
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private Image background;
        [SerializeField] private TileConfig tileConfig;
        [SerializeField] private Image icon;

        public void SetValue(int value)
        {
            TileData data = tileConfig.GetTileData(value);
            background.color = Color.black;

            if (data.icon != null)
            {
                valueText.gameObject.SetActive(false);
                icon.sprite = data.icon;
                icon.color = Color.white;
                icon.gameObject.SetActive(true);
            }
            else
            {
                icon.gameObject.SetActive(false);
                valueText.gameObject.SetActive(true);
                valueText.text = value == 0 ? "" : value.ToString();
            }
        }

        public void PlayMergeAnimation()
        {
            StartCoroutine(MergeAnimation());
        }

        private IEnumerator MergeAnimation()
        {
            Vector3 original = Vector3.one;
            Vector3 big = original * 1.2f;
            float duration = 0.1f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                transform.localScale = Vector3.Lerp(original, big, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            elapsed = 0f;

            while (elapsed < duration)
            {
                transform.localScale = Vector3.Lerp(big, original, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localScale = original;
        }

        public void PlayAppearAnimation()
        {
            StartCoroutine(StartScale());
        }

        private IEnumerator StartScale()
        {
            float elapsed = 0f;
            float duration = 0.1f;

            while (elapsed < duration)
            {
                transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            transform.localScale = Vector3.one;
        }
    }
}
