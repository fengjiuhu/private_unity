using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Consumes coins that enter the trigger and awards multiplied currency.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class MultiplierGate : MonoBehaviour
    {
        [SerializeField] private long multiplier = 10;
        [SerializeField] private CurrencyManager currencyManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private GateVisuals visuals;
        [SerializeField] private MultiplierFX fx;

        private void Reset()
        {
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        private void Awake()
        {
            visuals ??= GetComponentInChildren<GateVisuals>();
            fx ??= GetComponentInChildren<MultiplierFX>();
            visuals?.SetLabel((int)multiplier);
        }

        private void OnTriggerEnter(Collider other)
        {
            Coin coin = other.GetComponent<Coin>();
            if (coin == null)
            {
                return;
            }

            ProcessCoin(other.GetComponent<CoinCollector>());
        }

        public void ProcessCoin(CoinCollector collector)
        {
            if (collector == null)
            {
                return;
            }

            long reward = collector.GetComponent<Coin>().Value * multiplier;
            currencyManager ??= FindObjectOfType<CurrencyManager>();
            currencyManager?.AddGold(reward);
            uiManager?.ShowFloatingText($"+{reward}", collector.transform.position);
            fx?.Play();
            collector.CollectWithMultiplier((int)multiplier);
        }
    }
}
