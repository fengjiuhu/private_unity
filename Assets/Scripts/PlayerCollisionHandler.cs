using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Manages coin pushing, gate triggers, and boundary collisions.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerCollisionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;
        [SerializeField] private LayerMask coinLayer;
        [SerializeField] private float pushForceMultiplier = 1f;

        private void Awake()
        {
            stats ??= new PlayerStats();
        }

        private void OnCollisionStay(Collision collision)
        {
            if ((coinLayer.value & (1 << collision.gameObject.layer)) == 0)
            {
                return;
            }

            Rigidbody coinBody = collision.rigidbody;
            if (coinBody == null)
            {
                return;
            }

            Vector3 pushDir = collision.GetContact(0).point - transform.position;
            pushDir.y = 0f;
            pushDir = pushDir.normalized;
            coinBody.AddForce(pushDir * stats.pushForce * pushForceMultiplier, ForceMode.Acceleration);
        }
    }
}
