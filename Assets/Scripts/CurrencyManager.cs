using System;
using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Tracks player currencies and broadcasts change events for UI.
    /// </summary>
    public class CurrencyManager : MonoBehaviour
    {
        public event Action<long> OnGoldChanged;
        public event Action<long> OnGreenPotionChanged;
        public event Action<long> OnRedGemChanged;

        [SerializeField] private long startingGold = 100;
        [SerializeField] private long startingPotions = 10;
        [SerializeField] private long startingGems = 5;

        public long Gold { get; private set; }
        public long GreenPotion { get; private set; }
        public long RedGem { get; private set; }
        public long Energy => GreenPotion;
        public long Gems => RedGem;

        public void Initialize()
        {
            SaveSystem.LoadCurrency(out long gold, out long energy, out long gems);
            Gold = gold > 0 ? gold : startingGold;
            GreenPotion = energy > 0 ? energy : startingPotions;
            RedGem = gems > 0 ? gems : startingGems;

            RaiseAll();
            EventBus.OnSaveRequested += Save;
        }

        public void AddGold(long amount)
        {
            Gold += amount;
            OnGoldChanged?.Invoke(Gold);
            EventBus.CurrencyChanged(Gold, GreenPotion, RedGem);
            Save();
        }

        public bool SpendGold(long amount)
        {
            if (Gold < amount)
            {
                return false;
            }

            Gold -= amount;
            OnGoldChanged?.Invoke(Gold);
            EventBus.CurrencyChanged(Gold, GreenPotion, RedGem);
            Save();
            return true;
        }

        public void AddGreenPotion(long amount)
        {
            GreenPotion += amount;
            OnGreenPotionChanged?.Invoke(GreenPotion);
            EventBus.CurrencyChanged(Gold, GreenPotion, RedGem);
            Save();
        }

        public void AddRedGem(long amount)
        {
            RedGem += amount;
            OnRedGemChanged?.Invoke(RedGem);
            EventBus.CurrencyChanged(Gold, GreenPotion, RedGem);
            Save();
        }

        private void RaiseAll()
        {
            OnGoldChanged?.Invoke(Gold);
            OnGreenPotionChanged?.Invoke(GreenPotion);
            OnRedGemChanged?.Invoke(RedGem);
            EventBus.CurrencyChanged(Gold, GreenPotion, RedGem);
        }

        private void OnDestroy()
        {
            EventBus.OnSaveRequested -= Save;
        }

        private void Save()
        {
            SaveSystem.SaveCurrency(Gold, GreenPotion, RedGem);
        }
    }
}
