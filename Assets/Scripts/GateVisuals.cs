using UnityEngine;
using UnityEngine.UI;

namespace CoinPush
{
    /// <summary>
    /// Handles text and particle feedback for multiplier gates.
    /// </summary>
    public class GateVisuals : MonoBehaviour
    {
        [SerializeField] private Text label;
        [SerializeField] private ParticleSystem entryFx;

        public void SetLabel(int multiplier)
        {
            if (label != null)
            {
                label.text = $"x{multiplier}";
            }
        }

        public void PlayFX()
        {
            if (entryFx != null)
            {
                entryFx.Play();
            }
        }
    }
}
