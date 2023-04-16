using System;
using UnityEngine;

public class SetUpBackground : MonoBehaviour
{
    [SerializeField] private LoadAssetBundles assetBundles;

    private int currentIndex = 0;
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
