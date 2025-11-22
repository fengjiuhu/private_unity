using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Pulls nearby coins toward the player for satisfying collection.
    /// </summary>
    public class PlayerVacuum : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;
        [SerializeField] private LayerMask coinLayer;
        [SerializeField] private float force = 5f;

        private void Reset()
        {
            stats ??= new PlayerStats();
        }

        private void Update()
        {
            if (stats == null)
            {
                return;
            }

            Collider[] hits = Physics.OverlapSphere(transform.position, stats.vacuumRadius, coinLayer);
            foreach (Collider hit in hits)
            {
                if (hit.attachedRigidbody == null)
                {
                    continue;
                }

                Vector3 dir = (transform.position - hit.transform.position).normalized;
                hit.attachedRigidbody.AddForce(dir * force, ForceMode.Acceleration);
            }
        }

        public void UpdateStats(PlayerStats newStats)
        {
            stats = newStats;
            EventBus.VacuumUpdated(stats.vacuumRadius);
        }
    }
}
