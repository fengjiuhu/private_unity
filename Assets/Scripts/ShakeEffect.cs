using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Lightweight shake for camera or UI transforms.
    /// </summary>
    public class ShakeEffect : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float amplitude = 0.05f;
        [SerializeField] private float duration = 0.2f;
        private float timer;
        private Vector3 originalPos;
        private bool shaking;

        private void Awake()
        {
            target ??= transform;
            originalPos = target.localPosition;
        }

        public void Play()
        {
            timer = 0f;
            shaking = true;
        }

        private void Update()
        {
            if (!shaking)
            {
                return;
            }

            timer += Time.deltaTime;
            target.localPosition = originalPos + Random.insideUnitSphere * amplitude;
            if (timer >= duration)
            {
                shaking = false;
                target.localPosition = originalPos;
            }
        }
    }
}
