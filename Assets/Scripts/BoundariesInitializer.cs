using Core;
using UnityEngine;

public class BoundariesInitializer : Singleton<PointsManager>
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
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
}