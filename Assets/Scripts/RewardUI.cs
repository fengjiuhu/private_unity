using UnityEngine;
using UnityEngine.UI;

namespace CoinPush
{
    /// <summary>
    /// Handles rewarded button to grant bonus currency.
    /// </summary>
    public class RewardUI : MonoBehaviour
    {
        [SerializeField] private Button rewardButton;
        [SerializeField] private long rewardAmount = 500;
        [SerializeField] private CurrencyManager currencyManager;

        private void Awake()
        {
            currencyManager ??= FindObjectOfType<CurrencyManager>();
        }

        private void OnEnable()
        {
            if (rewardButton != null) rewardButton.onClick.AddListener(GrantReward);
        }

        private void OnDisable()
        {
            if (rewardButton != null) rewardButton.onClick.RemoveListener(GrantReward);
        }

        private void GrantReward()
        {
            currencyManager?.AddGold(rewardAmount);
            EventBus.CurrencyChanged(currencyManager?.Gold ?? 0, currencyManager?.Energy ?? 0, currencyManager?.Gems ?? 0);
        }
    }
}
