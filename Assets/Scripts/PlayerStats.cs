using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Holds player stat values and level for upgrade-driven progression.
    /// </summary>
    [System.Serializable]
    public class PlayerStats
    {
        public int level = 1;
        public float moveSpeed = 5f;
        public float pushForce = 10f;
        public float vacuumRadius = 3f;

        public void ApplyMultipliers(float speedMult, float pushMult, float vacuumMult)
        {
            moveSpeed *= speedMult;
            pushForce *= pushMult;
            vacuumRadius *= vacuumMult;
        }
    }
}
