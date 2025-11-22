using UnityEngine;
using UnityEngine.UI;

namespace CoinPush
{
    /// <summary>
    /// Shows debug stats for quick profiling on device.
    /// </summary>
    public class DebugPanel : MonoBehaviour
    {
        [SerializeField] private Text fpsText;
        [SerializeField] private Text coinsText;
        [SerializeField] private CoinSpawner spawner;

        private float timer;
        private int frames;

        private void Awake()
        {
            spawner ??= FindObjectOfType<CoinSpawner>();
        }

        private void Update()
        {
            frames++;
            timer += Time.deltaTime;
            if (timer >= 1f)
            {
                int fps = Mathf.RoundToInt(frames / timer);
                if (fpsText != null)
                {
                    fpsText.text = $"FPS: {fps}";
                }

                if (coinsText != null && spawner != null)
                {
                    coinsText.text = $"Coins: {spawner.ActiveCoinCount}";
                }

                timer = 0f;
                frames = 0;
            }
        }
    }
}
