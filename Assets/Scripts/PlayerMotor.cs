using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Handles Rigidbody-based vehicle motion.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField] private PlayerStats stats;
        private Rigidbody body;
        private Vector3 targetDirection;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            stats ??= new PlayerStats();
        }

        public void SetDirection(Vector2 moveInput)
        {
            targetDirection = new Vector3(moveInput.x, 0f, moveInput.y);
        }

        private void FixedUpdate()
        {
            if (targetDirection.sqrMagnitude > 0.001f)
            {
                Vector3 velocity = targetDirection.normalized * stats.moveSpeed;
                body.MovePosition(body.position + velocity * Time.fixedDeltaTime);
                transform.forward = targetDirection;
            }
        }

        public void UpdateStats(PlayerStats newStats)
        {
            stats = newStats;
        }

        public PlayerStats GetStats() => stats;
    }
}
