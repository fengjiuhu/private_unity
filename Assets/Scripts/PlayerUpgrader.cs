using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Applies upgrade multipliers to the player's stats using UpgradeManager data.
    /// </summary>
    public class PlayerUpgrader : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;
        [SerializeField] private UpgradeManager upgradeManager;
        [SerializeField] private PlayerMotor motor;
        [SerializeField] private PlayerVacuum vacuum;

        private void Awake()
        {
            stats ??= new PlayerStats();
            motor ??= GetComponent<PlayerMotor>();
            vacuum ??= GetComponent<PlayerVacuum>();
        }

        public void Initialize(CurrencyManager currency)
        {
            if (upgradeManager == null)
            {
                upgradeManager = FindObjectOfType<UpgradeManager>();
            }

            upgradeManager?.Initialize(stats, currency);
            ApplyStats();
        }

        public void ApplyStats()
        {
            motor?.UpdateStats(stats);
            vacuum?.UpdateStats(stats);
        }

        public void UpgradeSpeed() => TryUpgrade(stat => stats.moveSpeed = stat, upgradeManager?.TryUpgradeSpeed);
        public void UpgradePush() => TryUpgrade(stat => stats.pushForce = stat, upgradeManager?.TryUpgradePush);
        public void UpgradeVacuum() => TryUpgrade(stat => stats.vacuumRadius = stat, upgradeManager?.TryUpgradeVacuum);

        private void TryUpgrade(System.Action<float> setter, System.Func<float> upgradeCallback)
        {
            if (upgradeCallback == null)
            {
                return;
            }

            float newValue = upgradeCallback.Invoke();
            if (newValue <= 0f)
            {
                return;
            }

            setter?.Invoke(newValue);
            ApplyStats();
        }
    }
}
