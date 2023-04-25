using Core;
using UnityEngine;

public class SetUpBackground : Singleton<SetUpBackground>
{
    [SerializeField] private LoadAssetBundles assetBundles;

    private int currentIndex;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        spriteRenderer.sprite = assetBundles.backgroundSprites[currentIndex++];
    }

    public void ChangeBackground()
    {
        if (currentIndex < assetBundles.backgroundSprites.Length)
        {
            spriteRenderer.sprite = assetBundles.backgroundSprites[currentIndex++];
        }
        else
        {
            currentIndex = 0;
            spriteRenderer.sprite = assetBundles.backgroundSprites[currentIndex++];
        }
    }
}