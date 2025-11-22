using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Unlocks new map areas by spending currency.
    /// </summary>
    public class AreaUnlocker : MonoBehaviour
    {
        [SerializeField] private long unlockCost = 1000;
        [SerializeField] private CurrencyManager currencyManager;
        [SerializeField] private ZoneManager zoneManager;
        [SerializeField] private float newDensityMultiplier = 1.5f;

        public void TryUnlock()
        {
            currencyManager ??= FindObjectOfType<CurrencyManager>();
            if (currencyManager != null && currencyManager.SpendGold(unlockCost))
            {
                zoneManager?.SetDensity(newDensityMultiplier);
                gameObject.SetActive(false);
            }
        }
    }
}
