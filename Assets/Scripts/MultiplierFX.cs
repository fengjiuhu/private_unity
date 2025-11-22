using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Visual effects for multiplier gates.
    /// </summary>
    public class MultiplierFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem burst;

        public void Play()
        {
            burst?.Play();
        }
    }
}
