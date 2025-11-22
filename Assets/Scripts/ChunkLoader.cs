using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Placeholder for streaming larger maps in chunks.
    /// </summary>
    public class ChunkLoader : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private float loadDistance = 30f;

        public void Update()
        {
            // In a full game this would load/unload content; kept lightweight here.
            if (player == null)
            {
                return;
            }

            Vector3 pos = player.position;
            Debug.DrawLine(pos, pos + Vector3.forward * loadDistance, Color.green);
        }
    }
}
