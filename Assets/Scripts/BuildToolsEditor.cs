#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CoinPush
{
    /// <summary>
    /// Editor helper for one-click Android/iOS builds.
    /// </summary>
    public static class BuildToolsEditor
    {
        [MenuItem("CoinPush/Build/Android")]
        public static void BuildAndroid()
        {
            BuildPlayerOptions options = new BuildPlayerOptions
            {
                scenes = new[] { "Assets/Scenes/MainScene.unity" },
                locationPathName = "Builds/Android/CoinPush.apk",
                target = BuildTarget.Android,
                options = BuildOptions.Development
            };
            BuildPipeline.BuildPlayer(options);
        }

        [MenuItem("CoinPush/Build/iOS")]
        public static void BuildIOS()
        {
            BuildPlayerOptions options = new BuildPlayerOptions
            {
                scenes = new[] { "Assets/Scenes/MainScene.unity" },
                locationPathName = "Builds/iOS",
                target = BuildTarget.iOS,
                options = BuildOptions.Development
            };
            BuildPipeline.BuildPlayer(options);
        }
    }
}
#endif
