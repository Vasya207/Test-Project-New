using System;
using UnityEngine;

public class LoadAssetBundles : MonoBehaviour
{
    [SerializeField] private string path;
    [SerializeField] private string backgroundName;
    
    private AssetBundle loadedAssetBundle;
    public Sprite[] backgroundSprites { get; private set; }
    
    void Awake()
    {
        LoadAssetBundle(path);
        InstantiateObjectFromBundle(backgroundName);
    }

    void LoadAssetBundle(string assetBundleURL)
    {
        loadedAssetBundle = AssetBundle.LoadFromFile(assetBundleURL);
        
        //Debug.Log(myLoadedAssetBundle == null ? "false" : "true");
    }

    void InstantiateObjectFromBundle(string assetName)
    {
        backgroundSprites = loadedAssetBundle.LoadAllAssets<Sprite>();
        //backgroundSprite = loadedAssetBundle.LoadAsset<Sprite>(assetName);
        //var prefab = loadedAssetBundle.LoadAsset(assetName);
        foreach (var sprite in backgroundSprites) Instantiate(sprite);
    }
}
