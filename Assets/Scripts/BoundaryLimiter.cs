using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Keeps player inside defined bounds.
    /// </summary>
    public class BoundaryLimiter : MonoBehaviour
    {
        [SerializeField] private Vector2 bounds = new Vector2(25f, 25f);
        [SerializeField] private Transform target;

        private void Awake()
        {
            target ??= transform;
        }

        private void LateUpdate()
        {
            Vector3 pos = target.position;
            pos.x = Mathf.Clamp(pos.x, -bounds.x, bounds.x);
            pos.z = Mathf.Clamp(pos.z, -bounds.y, bounds.y);
            target.position = pos;
        }
    }
}
