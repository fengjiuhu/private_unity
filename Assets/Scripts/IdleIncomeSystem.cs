using System;
using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Generates online and offline idle income for a relaxing progression loop.
    /// </summary>
    public class IdleIncomeSystem : MonoBehaviour
    {
        [SerializeField] private CurrencyManager currencyManager;
        [SerializeField] private UIManager uiManager;
        [Tooltip("基础挂机每秒金币收益")]
        [SerializeField] private float goldPerSecond = 2f;
        [Tooltip("离线收益最大累计分钟数")]
        [SerializeField] private float offlineCapMinutes = 180f;

        private float timer;

        public void Initialize(CurrencyManager currency)
        {
            currencyManager = currency;
            GrantOfflineIncome();
        }

        private void Update()
        {
            if (currencyManager == null)
            {
                return;
            }

            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                timer -= 1f;
                currencyManager.AddGold(Mathf.RoundToInt(goldPerSecond));
            }
        }

        private void GrantOfflineIncome()
        {
            DateTime last = SaveSystem.LoadLastSessionTime();
            if (last == default)
            {
                SaveSystem.SaveLastSessionTime(DateTime.UtcNow);
                return;
            }

            TimeSpan elapsed = DateTime.UtcNow - last;
            double seconds = Math.Min(elapsed.TotalSeconds, offlineCapMinutes * 60f);
            long reward = (long)(seconds * goldPerSecond);
            if (reward > 0 && currencyManager != null)
            {
                currencyManager.AddGold(reward);
                if (uiManager != null)
                {
                    string title = LocalizationManager.Instance != null ? LocalizationManager.Instance.Translate("popup_idle_income", "离线收益") : "离线收益";
                    string body = LocalizationManager.Instance != null ? LocalizationManager.Instance.Translate("popup_idle_body", "欢迎回来！已自动收集") : "欢迎回来！已自动收集";
                    uiManager.ShowFloatingText($"{title}: +{reward}\n{body}", Vector3.zero);
                }
            }

            SaveSystem.SaveLastSessionTime(DateTime.UtcNow);
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveSystem.SaveLastSessionTime(DateTime.UtcNow);
            }
        }

        private void OnApplicationQuit()
        {
            SaveSystem.SaveLastSessionTime(DateTime.UtcNow);
        }
    }
}
