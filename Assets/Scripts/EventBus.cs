using System;

namespace CoinPush
{
    /// <summary>
    /// Lightweight event hub to decouple UI and gameplay systems.
    /// </summary>
    public static class EventBus
    {
        public static event Action<long, long, long> OnCurrencyChanged;
        public static event Action<string, int> OnUpgradeChanged;
        public static event Action OnSaveRequested;
        public static event Action<float> OnSpeedUpdated;
        public static event Action<float> OnPushUpdated;
        public static event Action<float> OnVacuumUpdated;

        public static void CurrencyChanged(long gold, long energy, long gems) => OnCurrencyChanged?.Invoke(gold, energy, gems);
        public static void UpgradeChanged(string id, int level) => OnUpgradeChanged?.Invoke(id, level);
        public static void SaveRequested() => OnSaveRequested?.Invoke();
        public static void SpeedUpdated(float value) => OnSpeedUpdated?.Invoke(value);
        public static void PushUpdated(float value) => OnPushUpdated?.Invoke(value);
        public static void VacuumUpdated(float value) => OnVacuumUpdated?.Invoke(value);
    }
}
