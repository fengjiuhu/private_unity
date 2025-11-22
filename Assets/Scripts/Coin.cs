using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Represents a simple coin with collection state.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Coin : MonoBehaviour
    {
        [SerializeField] private long value = 1;
        [SerializeField] private float autoCollectRadius = 1f;
        [SerializeField] private LayerMask playerLayer;

        private bool collected;
        private CoinCollector collector;

        public long Value => value;
        public bool IsCollected => collected;

        private void Awake()
        {
            collector = GetComponent<CoinCollector>();
        }

        private void OnEnable()
        {
            collected = false;
        }

        private void Update()
        {
            if (collected)
            {
                return;
            }

            Collider[] players = Physics.OverlapSphere(transform.position, autoCollectRadius, playerLayer);
            if (players.Length > 0)
            {
                Collect();
            }
        }

        public void Collect()
        {
            collected = true;
            gameObject.SetActive(false);
        }

        public void CollectWithMultiplier(int multiplier)
        {
            collector ??= GetComponent<CoinCollector>();
            if (collector != null)
            {
                collector.CollectWithMultiplier(multiplier);
            }
            else
            {
                Collect();
            }
        }
    }
}
