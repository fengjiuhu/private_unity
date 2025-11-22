using System.Collections.Generic;
using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Stores upgrade tracks and applies them to player stats and gates.
    /// </summary>
    public class UpgradeManager : MonoBehaviour
    {
        [System.Serializable]
        public class UpgradeTrack
        {
            public string id;
            public List<float> values = new();
            public List<int> costs = new();
        }

        [SerializeField] private List<UpgradeTrack> tracks = new();
        [SerializeField] private int defaultLevel = 0;

        private CurrencyManager currency;
        private PlayerStats stats;

        public void Initialize(PlayerStats playerStats, CurrencyManager currencyManager)
        {
            stats = playerStats ?? new PlayerStats();
            currency = currencyManager;
        }

        public float TryUpgradeSpeed() => TryUpgrade("speed", value => stats.moveSpeed = value);
        public float TryUpgradePush() => TryUpgrade("push", value => stats.pushForce = value);
        public float TryUpgradeVacuum() => TryUpgrade("vacuum", value => stats.vacuumRadius = value);

        private float TryUpgrade(string id, System.Action<float> setter)
        {
            UpgradeTrack track = tracks.Find(t => t.id == id);
            if (track == null)
            {
                return 0f;
            }

            int level = SaveSystem.LoadUpgradeLevel(id, defaultLevel);
            if (level >= track.values.Count - 1)
            {
                setter?.Invoke(track.values[Mathf.Min(level, track.values.Count - 1)]);
                return track.values[Mathf.Min(level, track.values.Count - 1)];
            }

            int cost = track.costs[Mathf.Clamp(level + 1, 0, track.costs.Count - 1)];
            if (currency != null && !currency.SpendGold(cost))
            {
                return 0f;
            }

            level++;
            SaveSystem.SaveUpgradeLevel(id, level);
            float newValue = track.values[Mathf.Clamp(level, 0, track.values.Count - 1)];
            setter?.Invoke(newValue);
            EventBus.UpgradeChanged(id, level);
            return newValue;
        }
    }
}
