using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Keeps coin rigidbodies within stable speed ranges for better performance.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CoinPhysicsOptimizer : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 10f;
        private Rigidbody body;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (body.velocity.sqrMagnitude > maxSpeed * maxSpeed)
            {
                body.velocity = body.velocity.normalized * maxSpeed;
            }
        }
    }
}
