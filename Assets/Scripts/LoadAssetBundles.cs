using UnityEngine;

public class LoadAssetBundles : MonoBehaviour
{
    [SerializeField] private string androidPath;
    [SerializeField] private string iOSPath;
    [SerializeField] private string windowsPath;
    [SerializeField] private string oldPath;
    
    private AssetBundle loadedAssetBundle;
    public Sprite[] backgroundSprites { get; private set; }
    
    void Awake()
    {
        LoadAssetBundle();
        InstantiateObjectFromBundle();
    }

    void LoadAssetBundle()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            loadedAssetBundle = AssetBundle.LoadFromFile(androidPath);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            loadedAssetBundle = AssetBundle.LoadFromFile(iOSPath);
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            loadedAssetBundle = AssetBundle.LoadFromFile(windowsPath);
        }
        else
        {
            loadedAssetBundle = AssetBundle.LoadFromFile(windowsPath);
        }
    }

    void InstantiateObjectFromBundle()
    {
        backgroundSprites = loadedAssetBundle.LoadAllAssets<Sprite>();
        foreach (var sprite in backgroundSprites) Instantiate(sprite);
    }
}
