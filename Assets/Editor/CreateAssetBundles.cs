using System.IO;
using UnityEditor;

public class CreateAssetBundles
{
    static private BuildTarget[] supportedTargets =
    {
        BuildTarget.iOS,
        BuildTarget.StandaloneWindows,
        BuildTarget.Android,
    };
    static private void BuildAssetBundle(BuildTarget target)
    {
        string assetBundleDirectory = "Assets/AssetBundles/" + target;
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, target);
    }
 
    [MenuItem("Assets/Build AssetBundles/Windows")]
    static void BuildAssetBundlesWindows()
    {
        BuildAssetBundle(BuildTarget.StandaloneWindows);
    }
 
    [MenuItem("Assets/Build AssetBundles/All")]
    static void BuildAssetBundlesAll()
    {
        foreach (BuildTarget target in supportedTargets)
        {
            BuildAssetBundle(target);
        }
    }
}