using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Plays a particle or sound when coin is collected.
    /// </summary>
    public class CoinPickupFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private AudioSource audioSource;

        public void Play()
        {
            particles?.Play();
            audioSource?.Play();
        }
    }
}
