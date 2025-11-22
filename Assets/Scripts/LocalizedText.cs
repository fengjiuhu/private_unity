using UnityEngine;
using UnityEngine.UI;

namespace CoinPush
{
    /// <summary>
    /// Binds a Text component to a localization key and refreshes on language changes.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string key;
        [SerializeField] private string fallback = "";

        private Text label;
        private LocalizationManager localization;

        private void Awake()
        {
            label = GetComponent<Text>();
            localization = LocalizationManager.Instance != null ? LocalizationManager.Instance : FindObjectOfType<LocalizationManager>();
        }

        private void OnEnable()
        {
            Refresh();
            if (localization != null)
            {
                localization.OnLanguageChanged += OnLanguageChanged;
            }
        }

        private void OnDisable()
        {
            if (localization != null)
            {
                localization.OnLanguageChanged -= OnLanguageChanged;
            }
        }

        public void Refresh()
        {
            if (label == null)
            {
                return;
            }

            if (localization == null)
            {
                label.text = fallback;
                return;
            }

            label.text = localization.Translate(key, fallback);
        }

        private void OnLanguageChanged(LocalizationManager.Language language)
        {
            Refresh();
        }
    }
}
