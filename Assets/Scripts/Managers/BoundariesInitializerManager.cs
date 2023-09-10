using Core;
using UnityEngine;

public class BoundariesInitializerManager : MonoBehaviour
{
    public Vector2 minBounds { get; private set; }
    public Vector2 maxBounds { get; private set; }

    private void Awake()
    {
        InitializeBoundaries();
    }

    private void InitializeBoundaries()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;
        minBounds = mainCamera.ViewportToWorldPoint(Vector2.zero);
        maxBounds = mainCamera.ViewportToWorldPoint(Vector2.one);
    }
}