using UnityEngine;
using UnityEngine.UI;

namespace CoinPush
{
    /// <summary>
    /// Displays gold, energy, and gems on HUD.
    /// </summary>
    public class CurrencyUI : MonoBehaviour
    {
        [SerializeField] private Text goldText;
        [SerializeField] private Text energyText;
        [SerializeField] private Text gemText;

        private void OnEnable()
        {
            EventBus.OnCurrencyChanged += UpdateDisplay;
        }

        private void OnDisable()
        {
            EventBus.OnCurrencyChanged -= UpdateDisplay;
        }

        public void UpdateDisplay(long gold, long energy, long gems)
        {
            if (goldText != null) goldText.text = gold.ToString();
            if (energyText != null) energyText.text = energy.ToString();
            if (gemText != null) gemText.text = gems.ToString();
        }
    }
}
