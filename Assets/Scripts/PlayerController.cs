using UnityEngine;
using UnityEngine.InputSystem;

namespace CoinPush
{
    /// <summary>
    /// Handles input routing to movement, vacuum, and collision subsystems.
    /// </summary>
    [RequireComponent(typeof(PlayerMotor))]
    [RequireComponent(typeof(PlayerVacuum))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private MobileJoystick mobileJoystick;
        [SerializeField] private PlayerStats stats;
        [SerializeField] private PlayerCollisionHandler collisionHandler;

        private PlayerMotor motor;
        private PlayerVacuum vacuum;
        private Vector2 moveInput;

        private void Awake()
        {
            motor = GetComponent<PlayerMotor>();
            vacuum = GetComponent<PlayerVacuum>();
            stats ??= new PlayerStats();
            collisionHandler ??= GetComponent<PlayerCollisionHandler>();
        }

        private void Update()
        {
            if (mobileJoystick != null)
            {
                moveInput = mobileJoystick.Direction;
            }

            motor.SetDirection(moveInput);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        public void ApplySpeedMultiplier(float multiplier)
        {
            stats.moveSpeed *= multiplier;
            motor.UpdateStats(stats);
            EventBus.SpeedUpdated(stats.moveSpeed);
        }

        public void ApplyPushForceMultiplier(float multiplier)
        {
            stats.pushForce *= multiplier;
            collisionHandler = collisionHandler == null ? GetComponent<PlayerCollisionHandler>() : collisionHandler;
            if (collisionHandler != null)
            {
                collisionHandler.enabled = true;
            }
            EventBus.PushUpdated(stats.pushForce);
        }

        public void ApplyVacuumRadiusMultiplier(float multiplier)
        {
            stats.vacuumRadius *= multiplier;
            vacuum.UpdateStats(stats);
        }
    }
}
