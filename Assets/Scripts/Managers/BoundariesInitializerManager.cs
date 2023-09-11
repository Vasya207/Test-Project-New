using Core;
using UnityEngine;

public class BoundariesInitializerManager : MonoBehaviour
{
    public Vector2 MinBounds { get; private set; }
    public Vector2 MaxBounds { get; private set; }

    private void Awake()
    {
        InitializeBoundaries();
    }

    private void InitializeBoundaries()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;
        MinBounds = mainCamera.ViewportToWorldPoint(Vector2.zero);
        MaxBounds = mainCamera.ViewportToWorldPoint(Vector2.one);
    }
}