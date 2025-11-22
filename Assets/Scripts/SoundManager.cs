using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Central audio controller for SFX and music.
    /// </summary>
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        private bool muted;

        public void PlaySfx(AudioClip clip)
        {
            if (muted || clip == null)
            {
                return;
            }

            sfxSource?.PlayOneShot(clip);
        }

        public void SetMuted(bool mute)
        {
            muted = mute;
            if (musicSource != null)
            {
                musicSource.mute = mute;
            }
            if (sfxSource != null)
            {
                sfxSource.mute = mute;
            }
        }
    }
}
