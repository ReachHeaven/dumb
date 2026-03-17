using Game.Data;
using TMPro;
using UnityEngine;
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
            background.color = data.color;

            if (data.icon != null)
            {
                valueText.gameObject.SetActive(false);
                icon.sprite = data.icon;
                icon.gameObject.SetActive(true);
            }
            else
            {
                icon.gameObject.SetActive(false);
                valueText.gameObject.SetActive(true);
                valueText.text = value == 0 ? "" : value.ToString();
            }
        }
    }
}
