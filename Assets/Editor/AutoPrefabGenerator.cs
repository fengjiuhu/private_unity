#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CoinPush.Editor
{
    /// <summary>
    /// Creates placeholder prefabs for the demo on demand.
    /// </summary>
    public static class AutoPrefabGenerator
    {
        [MenuItem("CoinPush/Generate Prefabs")]
        public static void Generate()
        {
            EnsureFolder("Assets/Prefabs");

            CreateCoinPrefab();
            CreatePlayerPrefab();
            CreateGatePrefab();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void CreateCoinPrefab()
        {
            string path = "Assets/Prefabs/Coin.prefab";
            if (System.IO.File.Exists(path))
            {
                return;
            }

            GameObject coin = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            coin.transform.localScale = new Vector3(0.3f, 0.1f, 0.3f);
            coin.AddComponent<Rigidbody>();
            coin.AddComponent<Coin>();
            PrefabUtility.SaveAsPrefabAsset(coin, path);
            Object.DestroyImmediate(coin);
        }

        private static void CreatePlayerPrefab()
        {
            string path = "Assets/Prefabs/PlayerVehicle.prefab";
            if (System.IO.File.Exists(path))
            {
                return;
            }

            GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            player.name = "PlayerVehicle";
            Rigidbody rb = player.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            player.AddComponent<PlayerController>();
            PrefabUtility.SaveAsPrefabAsset(player, path);
            Object.DestroyImmediate(player);
        }

        private static void CreateGatePrefab()
        {
            string path = "Assets/Prefabs/MultiplierGate.prefab";
            if (System.IO.File.Exists(path))
            {
                return;
            }

            GameObject gate = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gate.name = "MultiplierGate";
            gate.transform.localScale = new Vector3(3f, 2f, 0.5f);
            Collider gateCollider = gate.GetComponent<Collider>();
            gateCollider.isTrigger = true;
            gate.AddComponent<MultiplierGate>();
            PrefabUtility.SaveAsPrefabAsset(gate, path);
            Object.DestroyImmediate(gate);
        }

        private static void EnsureFolder(string folder)
        {
            if (!AssetDatabase.IsValidFolder(folder))
            {
                string parent = System.IO.Path.GetDirectoryName(folder);
                string name = System.IO.Path.GetFileName(folder);
                AssetDatabase.CreateFolder(parent, name);
            }
        }
    }
}
#endif
