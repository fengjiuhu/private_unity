using System.Collections.Generic;
using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Simple generic object pooler for coins, particles, and other reusable objects.
    /// </summary>
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string id;
            public GameObject prefab;
            public int initialSize = 50;
        }

        [SerializeField] private List<Pool> pools = new();

        private readonly Dictionary<string, Queue<GameObject>> lookup = new();

        private void Awake()
        {
            foreach (Pool pool in pools)
            {
                Queue<GameObject> queue = new Queue<GameObject>();
                for (int i = 0; i < pool.initialSize; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, transform);
                    obj.SetActive(false);
                    queue.Enqueue(obj);
                }

                lookup[pool.id] = queue;
            }
        }

        public GameObject Get(string id)
        {
            if (!lookup.TryGetValue(id, out Queue<GameObject> queue))
            {
                Debug.LogWarning($"Pool id {id} not found");
                return null;
            }

            GameObject obj = queue.Count > 0 ? queue.Dequeue() : null;
            if (obj == null)
            {
                return null;
            }

            obj.SetActive(true);
            return obj;
        }

        public void Return(string id, GameObject obj)
        {
            if (obj == null)
            {
                return;
            }

            obj.SetActive(false);
            if (!lookup.ContainsKey(id))
            {
                lookup[id] = new Queue<GameObject>();
            }

            lookup[id].Enqueue(obj);
        }
    }
}
