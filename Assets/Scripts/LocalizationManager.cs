using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Simple localization hub with built-in中英 language pack.
    /// Keeps a dictionary of keys and raises language change events so UI can refresh dynamically.
    /// </summary>
    public class LocalizationManager : MonoBehaviour
    {
        public enum Language
        {
            ChineseSimplified,
            EnglishUS
        }

        [Serializable]
        public class LocalizationEntry
        {
            public string key;
            public string chinese;
            public string english;
        }

        [Header("Built-in Language Pack")]
        [SerializeField] private Language defaultLanguage = Language.ChineseSimplified;
        [SerializeField] private List<LocalizationEntry> entries = new();

        private readonly Dictionary<string, LocalizationEntry> table = new();
        public event Action<Language> OnLanguageChanged;

        public Language CurrentLanguage { get; private set; }

        private static LocalizationManager instance;
        public static LocalizationManager Instance => instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);

            SeedDefaults();
            BuildLookup();
            LoadLanguage();
        }

        /// <summary>
        /// Returns the localized string for the key using the active language.
        /// </summary>
        public string Translate(string key, string fallback = "")
        {
            if (string.IsNullOrEmpty(key))
            {
                return fallback;
            }

            if (table.TryGetValue(key, out LocalizationEntry entry))
            {
                return CurrentLanguage == Language.ChineseSimplified ? entry.chinese : entry.english;
            }

            return fallback;
        }

        public void SetLanguage(Language language)
        {
            if (CurrentLanguage == language)
            {
                return;
            }

            CurrentLanguage = language;
            SaveSystem.SaveLanguage(language.ToString());
            OnLanguageChanged?.Invoke(language);
        }

        private void LoadLanguage()
        {
            string saved = SaveSystem.LoadLanguage();
            if (!string.IsNullOrEmpty(saved) && Enum.TryParse(saved, out Language loaded))
            {
                CurrentLanguage = loaded;
            }
            else
            {
                CurrentLanguage = defaultLanguage;
            }
        }

        private void BuildLookup()
        {
            table.Clear();
            foreach (LocalizationEntry entry in entries.Where(e => !string.IsNullOrEmpty(e.key)))
            {
                table[entry.key] = entry;
            }
        }

        /// <summary>
        /// Fill a default bilingual pack so the template is usable without manual authoring.
        /// </summary>
        private void SeedDefaults()
        {
            if (entries.Count > 0)
            {
                return;
            }

            entries.AddRange(new[]
            {
                new LocalizationEntry { key = "currency_gold", chinese = "金币", english = "Gold" },
                new LocalizationEntry { key = "currency_energy", chinese = "能量", english = "Energy" },
                new LocalizationEntry { key = "currency_gem", chinese = "宝石", english = "Gems" },
                new LocalizationEntry { key = "upgrade_speed", chinese = "速度升级", english = "Speed" },
                new LocalizationEntry { key = "upgrade_push", chinese = "推力升级", english = "Push" },
                new LocalizationEntry { key = "upgrade_vacuum", chinese = "吸力升级", english = "Vacuum" },
                new LocalizationEntry { key = "button_upgrade", chinese = "升级", english = "Upgrade" },
                new LocalizationEntry { key = "button_settings", chinese = "设置", english = "Settings" },
                new LocalizationEntry { key = "button_shop", chinese = "商店", english = "Shop" },
                new LocalizationEntry { key = "button_missions", chinese = "任务", english = "Missions" },
                new LocalizationEntry { key = "button_rewards", chinese = "奖励", english = "Rewards" },
                new LocalizationEntry { key = "label_level", chinese = "等级", english = "Level" },
                new LocalizationEntry { key = "label_language", chinese = "语言", english = "Language" },
                new LocalizationEntry { key = "label_sound", chinese = "音效", english = "Sound" },
                new LocalizationEntry { key = "label_haptic", chinese = "震动", english = "Haptics" },
                new LocalizationEntry { key = "popup_idle_income", chinese = "离线收益", english = "Offline Income" },
                new LocalizationEntry { key = "popup_idle_body", chinese = "欢迎回来！已自动收集", english = "Welcome back! Auto-collected" }
            });
        }
    }
}
