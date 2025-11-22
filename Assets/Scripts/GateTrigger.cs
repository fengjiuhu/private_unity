using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Detects coins entering a multiplier gate.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class GateTrigger : MonoBehaviour
    {
        [SerializeField] private MultiplierGate gate;

        private void Awake()
        {
            if (gate == null)
            {
                gate = GetComponentInParent<MultiplierGate>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CoinCollector collector = other.GetComponent<CoinCollector>();
            if (collector == null)
            {
                return;
            }

            gate?.ProcessCoin(collector);
        }
    }
}
