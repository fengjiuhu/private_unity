using System.Collections.Generic;
using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Holds upgrade levels and costs for vehicle stats and gate multipliers.
    /// </summary>
    public class UpgradeSystem : MonoBehaviour
    {
        [System.Serializable]
        public class UpgradeLevel
        {
            public int level = 1;
            public int cost = 100;
            public float value = 1f;
        }

        [Header("Speed")]
        [SerializeField] private List<UpgradeLevel> speedLevels = new();
        [Header("Push Force")]
        [SerializeField] private List<UpgradeLevel> pushForceLevels = new();
        [Header("Vacuum Radius")]
        [SerializeField] private List<UpgradeLevel> vacuumRadiusLevels = new();

        private int speedIndex;
        private int pushForceIndex;
        private int vacuumIndex;

        private CurrencyManager currency;
        private PlayerController player;

        public void Initialize(CurrencyManager currencyManager, PlayerController playerController)
        {
            currency = currencyManager;
            player = playerController;
            ApplyStats();
        }

        public bool TryUpgradeSpeed()
        {
            return TryUpgrade(speedLevels, ref speedIndex, player.ApplySpeedMultiplier);
        }

        public bool TryUpgradePushForce()
        {
            return TryUpgrade(pushForceLevels, ref pushForceIndex, player.ApplyPushForceMultiplier);
        }

        public bool TryUpgradeVacuum()
        {
            return TryUpgrade(vacuumRadiusLevels, ref vacuumIndex, player.ApplyVacuumRadiusMultiplier);
        }

        private bool TryUpgrade(List<UpgradeLevel> levels, ref int index, System.Action<float> applyCallback)
        {
            if (index >= levels.Count - 1)
            {
                return false;
            }

            UpgradeLevel next = levels[index + 1];
            if (!currency.SpendGold(next.cost))
            {
                return false;
            }

            index++;
            applyCallback?.Invoke(next.value);
            return true;
        }

        public float GetCurrentValue(List<UpgradeLevel> levels, int index)
        {
            if (levels == null || levels.Count == 0)
            {
                return 0f;
            }

            return levels[Mathf.Clamp(index, 0, levels.Count - 1)].value;
        }

        private void ApplyStats()
        {
            player.ApplySpeedMultiplier(GetCurrentValue(speedLevels, speedIndex));
            player.ApplyPushForceMultiplier(GetCurrentValue(pushForceLevels, pushForceIndex));
            player.ApplyVacuumRadiusMultiplier(GetCurrentValue(vacuumRadiusLevels, vacuumIndex));
        }
    }
}
