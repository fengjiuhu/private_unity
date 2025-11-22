#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Simple editor window to place gates and coin clusters.
    /// </summary>
    public class LevelEditorWindow : EditorWindow
    {
        private GameObject gatePrefab;
        private GameObject coinPrefab;

        [MenuItem("CoinPush/Level Editor")]
        public static void ShowWindow()
        {
            GetWindow<LevelEditorWindow>("Level Editor");
        }

        private void OnGUI()
        {
            GUILayout.Label("Place Prefabs", EditorStyles.boldLabel);
            gatePrefab = (GameObject)EditorGUILayout.ObjectField("Gate Prefab", gatePrefab, typeof(GameObject), false);
            coinPrefab = (GameObject)EditorGUILayout.ObjectField("Coin Prefab", coinPrefab, typeof(GameObject), false);

            if (GUILayout.Button("Spawn Gate"))
            {
                Spawn(gatePrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)));
            }

            if (GUILayout.Button("Spawn Coin Cluster"))
            {
                SpawnCluster();
            }
        }

        private void Spawn(GameObject prefab, Vector3 position)
        {
            if (prefab == null)
            {
                return;
            }

            GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            obj.transform.position = position;
            Undo.RegisterCreatedObjectUndo(obj, "Spawned object");
        }

        private void SpawnCluster()
        {
            if (coinPrefab == null)
            {
                return;
            }

            for (int i = 0; i < 25; i++)
            {
                Vector3 pos = new Vector3(Random.Range(-5, 5), 0.5f, Random.Range(-5, 5));
                Spawn(coinPrefab, pos);
            }
        }
    }
}
#endif
