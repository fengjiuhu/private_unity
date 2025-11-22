using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace CoinPush
{
    /// <summary>
    /// Basic settings toggles for audio and haptics.
    /// </summary>
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Toggle hapticToggle;
        [SerializeField] private Dropdown languageDropdown;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private HapticManager hapticManager;
        [SerializeField] private LocalizationManager localizationManager;

        private void Awake()
        {
            soundManager ??= FindObjectOfType<SoundManager>();
            hapticManager ??= FindObjectOfType<HapticManager>();
            localizationManager ??= FindObjectOfType<LocalizationManager>();
        }

        private void OnEnable()
        {
            if (soundToggle != null) soundToggle.onValueChanged.AddListener(OnSoundChanged);
            if (hapticToggle != null) hapticToggle.onValueChanged.AddListener(OnHapticChanged);
            if (languageDropdown != null)
            {
                languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
                BuildLanguageDropdown();
            }

            if (localizationManager != null)
            {
                localizationManager.OnLanguageChanged += SyncLanguageDropdown;
            }
        }

        private void OnDisable()
        {
            if (soundToggle != null) soundToggle.onValueChanged.RemoveListener(OnSoundChanged);
            if (hapticToggle != null) hapticToggle.onValueChanged.RemoveListener(OnHapticChanged);
            if (languageDropdown != null) languageDropdown.onValueChanged.RemoveListener(OnLanguageChanged);
            if (localizationManager != null) localizationManager.OnLanguageChanged -= SyncLanguageDropdown;
        }

        private void OnSoundChanged(bool on) => soundManager?.SetMuted(!on);
        private void OnHapticChanged(bool on) => hapticManager?.SetEnabled(on);

        private void BuildLanguageDropdown()
        {
            List<string> options = new List<string> { "中文 (简体)", "English" };
            languageDropdown.ClearOptions();
            languageDropdown.AddOptions(options);

            int index = localizationManager != null && localizationManager.CurrentLanguage == LocalizationManager.Language.EnglishUS ? 1 : 0;
            languageDropdown.SetValueWithoutNotify(index);
        }

        private void OnLanguageChanged(int index)
        {
            if (localizationManager == null)
            {
                return;
            }

            LocalizationManager.Language lang = index == 0 ? LocalizationManager.Language.ChineseSimplified : LocalizationManager.Language.EnglishUS;
            localizationManager.SetLanguage(lang);
        }

        private void SyncLanguageDropdown(LocalizationManager.Language language)
        {
            if (languageDropdown == null)
            {
                return;
            }

            languageDropdown.SetValueWithoutNotify(language == LocalizationManager.Language.EnglishUS ? 1 : 0);
        }
    }
}
