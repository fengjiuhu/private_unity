using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Cleans up coins that fall outside the play area.
    /// </summary>
    public class CoinDespawnArea : MonoBehaviour
    {
        [SerializeField] private string poolId = "coins";

        private ObjectPooler pooler;

        private void Start()
        {
            pooler = FindObjectOfType<ObjectPooler>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Coin coin = other.GetComponent<Coin>();
            if (coin == null)
            {
                return;
            }

            if (pooler != null)
            {
                pooler.Return(poolId, coin.gameObject);
            }
            else
            {
                Destroy(coin.gameObject);
            }
        }
    }
}
