using System;
using Core;
using UnityEngine;

public class CircleCleaner : Singleton<CircleCleaner>
{
    private float yPositionOffset = 1f;
    private BoundariesInitializer boundariesInitializer;

    private Action<CircleCleaner> _killAction;

    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        boundariesInitializer = GetComponentInParent<BoundariesInitializer>();
        transform.position = new Vector2(0, boundariesInitializer.minBounds.y - yPositionOffset);
        transform.localScale = new Vector2(boundariesInitializer.maxBounds.x, 1);
    }
}