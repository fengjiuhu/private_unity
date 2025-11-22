using UnityEngine;
using UnityEngine.UI;

namespace CoinPush
{
    /// <summary>
    /// Basic settings toggles for audio and haptics.
    /// </summary>
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Toggle hapticToggle;
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private HapticManager hapticManager;

        private void Awake()
        {
            soundManager ??= FindObjectOfType<SoundManager>();
            hapticManager ??= FindObjectOfType<HapticManager>();
        }

        private void OnEnable()
        {
            if (soundToggle != null) soundToggle.onValueChanged.AddListener(OnSoundChanged);
            if (hapticToggle != null) hapticToggle.onValueChanged.AddListener(OnHapticChanged);
        }

        private void OnDisable()
        {
            if (soundToggle != null) soundToggle.onValueChanged.RemoveListener(OnSoundChanged);
            if (hapticToggle != null) hapticToggle.onValueChanged.RemoveListener(OnHapticChanged);
        }

        private void OnSoundChanged(bool on) => soundManager?.SetMuted(!on);
        private void OnHapticChanged(bool on) => hapticManager?.SetEnabled(on);
    }
}
