using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Controls density and unlock progression of play zones.
    /// </summary>
    public class ZoneManager : MonoBehaviour
    {
        [SerializeField] private CoinSpawner spawner;
        [SerializeField] private float densityMultiplier = 1f;

        private void Awake()
        {
            spawner ??= FindObjectOfType<CoinSpawner>();
        }

        public void SetDensity(float multiplier)
        {
            densityMultiplier = multiplier;
            if (spawner != null)
            {
                spawner.SetDensityMultiplier(densityMultiplier);
            }
        }
    }
}
