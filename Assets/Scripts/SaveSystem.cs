using System.Collections.Generic;
using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Lightweight persistence helper built on PlayerPrefs for demo purposes.
    /// Stores currency and upgrade levels with simple JSON payloads for extensibility.
    /// </summary>
    public static class SaveSystem
    {
        private const string CurrencyKey = "Currency_Save";
        private const string UpgradeKey = "Upgrade_Save";

        [System.Serializable]
        private class CurrencyData
        {
            public long gold;
            public long energy;
            public long gems;
        }

        [System.Serializable]
        private class UpgradeData
        {
            public Dictionary<string, int> levels = new();
        }

        public static void SaveCurrency(long gold, long energy, long gems)
        {
            CurrencyData data = new CurrencyData { gold = gold, energy = energy, gems = gems };
            PlayerPrefs.SetString(CurrencyKey, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }

        public static void LoadCurrency(out long gold, out long energy, out long gems)
        {
            gold = energy = gems = 0;
            if (!PlayerPrefs.HasKey(CurrencyKey))
            {
                return;
            }

            CurrencyData data = JsonUtility.FromJson<CurrencyData>(PlayerPrefs.GetString(CurrencyKey));
            gold = data.gold;
            energy = data.energy;
            gems = data.gems;
        }

        public static void SaveUpgradeLevel(string id, int level)
        {
            UpgradeData data = GetUpgradeData();
            data.levels[id] = level;
            PlayerPrefs.SetString(UpgradeKey, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }

        public static int LoadUpgradeLevel(string id, int defaultLevel = 0)
        {
            UpgradeData data = GetUpgradeData();
            return data.levels.TryGetValue(id, out int level) ? level : defaultLevel;
        }

        private static UpgradeData GetUpgradeData()
        {
            if (!PlayerPrefs.HasKey(UpgradeKey))
            {
                return new UpgradeData();
            }

            return JsonUtility.FromJson<UpgradeData>(PlayerPrefs.GetString(UpgradeKey));
        }
    }
}
