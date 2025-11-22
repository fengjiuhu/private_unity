using System.Collections.Generic;
using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Pools floating texts for performance.
    /// </summary>
    public class FloatingTextController : MonoBehaviour
    {
        [SerializeField] private UINumberPopup popupPrefab;
        [SerializeField] private int poolSize = 10;
        private readonly Queue<UINumberPopup> pool = new();

        private void Awake()
        {
            for (int i = 0; i < poolSize; i++)
            {
                UINumberPopup popup = Instantiate(popupPrefab, transform);
                popup.gameObject.SetActive(false);
                pool.Enqueue(popup);
            }
        }

        public void ShowValue(string value)
        {
            if (pool.Count == 0)
            {
                return;
            }

            UINumberPopup popup = pool.Dequeue();
            popup.gameObject.SetActive(true);
            popup.SetValue(value);
            pool.Enqueue(popup);
        }
    }
}
