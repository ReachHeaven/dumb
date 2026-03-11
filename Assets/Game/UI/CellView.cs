using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private Image background;

        public void SetValue(int value)
        {
            valueText.text = value == 0 ? "" : value.ToString();
            background.color = GetColor(value);
        }

        private Color GetColor(int value)
        {
            return value switch
            {
                0 => new Color(0.804f, 0.757f, 0.706f), // #CDC1B4
                2 => new Color(0.933f, 0.894f, 0.855f), // #EEE4DA
                4 => new Color(0.929f, 0.878f, 0.784f), // #EDE0C8
                8 => new Color(0.949f, 0.694f, 0.475f), // #F2B179
                16 => new Color(0.961f, 0.584f, 0.388f), // #F59563
                32 => new Color(0.965f, 0.486f, 0.373f), // #F67C5F
                64 => new Color(0.965f, 0.369f, 0.231f), // #F65E3B
                128 => new Color(0.929f, 0.812f, 0.447f), // #EDCF72
                256 => new Color(0.929f, 0.800f, 0.380f), // #EDCC61
                512 => new Color(0.929f, 0.784f, 0.314f), // #EDC850
                1024 => new Color(0.929f, 0.769f, 0.247f), // #EDC53F
                2048 => new Color(0.929f, 0.753f, 0.180f), // #EDC22E
                _ => new Color(0.235f, 0.227f, 0.196f), // #3C3A32
            };
        }
    }
}
