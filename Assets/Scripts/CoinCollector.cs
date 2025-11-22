using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Marks coins collected either by player vacuum or multiplier gates.
    /// </summary>
    [RequireComponent(typeof(Coin))]
    public class CoinCollector : MonoBehaviour
    {
        private Coin coin;
        private CurrencyManager currencyManager;

        private void Awake()
        {
            coin = GetComponent<Coin>();
        }

        private void Start()
        {
            currencyManager = FindObjectOfType<CurrencyManager>();
        }

        public void CollectWithMultiplier(int multiplier)
        {
            if (coin == null)
            {
                return;
            }

            long earnings = coin.Value * multiplier;
            currencyManager?.AddGold(earnings);
            coin.Collect();
            EventBus.CurrencyChanged(currencyManager?.Gold ?? 0, currencyManager?.Energy ?? 0, currencyManager?.Gems ?? 0);
        }

        public void CollectDirect()
        {
            currencyManager?.AddGold(coin.Value);
            coin.Collect();
            EventBus.CurrencyChanged(currencyManager?.Gold ?? 0, currencyManager?.Energy ?? 0, currencyManager?.Gems ?? 0);
        }
    }
}
