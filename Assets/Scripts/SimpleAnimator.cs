using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Quick utility for looping scale/alpha animations.
    /// </summary>
    public class SimpleAnimator : MonoBehaviour
    {
        [SerializeField] private AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        [SerializeField] private float duration = 1f;
        [SerializeField] private Vector3 scaleRange = new Vector3(0.9f, 1.1f, 1f);
        private float timer;
        private Vector3 baseScale;

        private void Awake()
        {
            baseScale = transform.localScale;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            float t = (timer % duration) / duration;
            float eval = curve.Evaluate(t);
            transform.localScale = baseScale * Mathf.Lerp(scaleRange.x, scaleRange.y, eval);
        }
    }
}
