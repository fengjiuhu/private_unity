using UnityEngine;
using UnityEngine.UI;

namespace CoinPush
{
    /// <summary>
    /// Shows vehicle level and passive income multiplier.
    /// </summary>
    public class LevelPanelUI : MonoBehaviour
    {
        [SerializeField] private Text levelText;
        [SerializeField] private Text multiplierText;

        public void UpdateLevel(int level, float multiplier)
        {
            if (levelText != null) levelText.text = $"LV {level}";
            if (multiplierText != null) multiplierText.text = $"x{multiplier:0.0}";
        }
    }
}
