using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Manages light mobile haptics.
    /// </summary>
    public class HapticManager : MonoBehaviour
    {
        private bool enabledHaptics = true;

        public void SetEnabled(bool enabled)
        {
            enabledHaptics = enabled;
        }

        public void Pulse()
        {
#if UNITY_ANDROID || UNITY_IOS
            if (enabledHaptics)
            {
                Handheld.Vibrate();
            }
#endif
        }
    }
}
