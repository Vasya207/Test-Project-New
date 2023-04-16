using System;
using UnityEngine;

public class LoadAssetBundles : MonoBehaviour
{
    [SerializeField] private string path;
    
    private AssetBundle loadedAssetBundle;
    public Sprite[] backgroundSprites { get; private set; }
    
    void Awake()
    {
        LoadAssetBundle(path);
        InstantiateObjectFromBundle();
    }

    void LoadAssetBundle(string assetBundleURL)
    {
        loadedAssetBundle = AssetBundle.LoadFromFile(assetBundleURL);
    }

    void InstantiateObjectFromBundle()
    {
        backgroundSprites = loadedAssetBundle.LoadAllAssets<Sprite>();
        foreach (var sprite in backgroundSprites) Instantiate(sprite);
    }
}
