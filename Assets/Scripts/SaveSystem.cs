using System;
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
        private const string LanguageKey = "Language_Save";
        private const string LastSessionKey = "LastSession_Time";

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

        public static void SaveLanguage(string languageCode)
        {
            PlayerPrefs.SetString(LanguageKey, languageCode);
            PlayerPrefs.Save();
        }

        public static string LoadLanguage()
        {
            return PlayerPrefs.GetString(LanguageKey, string.Empty);
        }

        public static void SaveLastSessionTime(DateTime time)
        {
            PlayerPrefs.SetString(LastSessionKey, time.ToBinary().ToString());
            PlayerPrefs.Save();
        }

        public static DateTime LoadLastSessionTime()
        {
            if (!PlayerPrefs.HasKey(LastSessionKey))
            {
                return default;
            }

            if (long.TryParse(PlayerPrefs.GetString(LastSessionKey), out long data))
            {
                return DateTime.FromBinary(data);
            }

            return default;
        }
    }
}
