using System.Collections.Generic;
using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Spawns and recycles coins in the scene to maintain density.
    /// </summary>
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private Coin coinPrefab;
        [SerializeField] private string poolId = "coins";
        [SerializeField] private int initialCount = 500;
        [SerializeField] private Vector2 spawnAreaSize = new Vector2(20f, 20f);
        [SerializeField] private float respawnDelay = 2f;

        private readonly List<Coin> coins = new();
        private float timer;
        private float densityMultiplier = 1f;
        private ObjectPooler pooler;

        public int ActiveCoinCount => coins.Count;

        public void BeginSpawning()
        {
            pooler = FindObjectOfType<ObjectPooler>();
            int targetCount = Mathf.RoundToInt(initialCount * densityMultiplier);
            for (int i = 0; i < targetCount; i++)
            {
                SpawnCoin();
            }
        }

        public void SetDensityMultiplier(float value)
        {
            densityMultiplier = Mathf.Max(0.1f, value);
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= respawnDelay)
            {
                timer = 0f;
                SpawnCoin();
            }
        }

        private void SpawnCoin()
        {
            Vector3 pos = new Vector3(
                Random.Range(-spawnAreaSize.x * 0.5f, spawnAreaSize.x * 0.5f),
                0.5f,
                Random.Range(-spawnAreaSize.y * 0.5f, spawnAreaSize.y * 0.5f));

            GameObject coinObj;
            if (pooler != null)
            {
                coinObj = pooler.Get(poolId);
                if (coinObj != null)
                {
                    coinObj.transform.SetPositionAndRotation(pos, Quaternion.identity);
                }
                else
                {
                    coinObj = Instantiate(coinPrefab.gameObject, pos, Quaternion.identity, transform);
                }
            }
            else
            {
                coinObj = Instantiate(coinPrefab.gameObject, pos, Quaternion.identity, transform);
            }

            Coin coin = coinObj.GetComponent<Coin>();
            if (!coins.Contains(coin))
            {
                coins.Add(coin);
            }
        }
    }
}
