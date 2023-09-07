using System;
using Core;
using UnityEngine;
using Zenject;

public class CircleCleaner : MonoBehaviour
{
    [Inject] private BoundariesInitializer boundariesInitializer;
    [Inject] private CircleFactory circleFactory;
    //[Inject] private CircleSpawner circleSpawner;
    
    private float yPositionOffset = 5f;
    private Action<CircleCleaner> _killAction;

    private void Start()
    {
        SetUp();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var circle = other.GetComponent<Circle>();
        if (circle != null)
        {
            circleFactory.DeactivateCircle(circle);
        }
    }

    private void SetUp()
    {
        //boundariesInitializer = GetComponentInParent<BoundariesInitializer>();
        transform.position = new Vector2(0, boundariesInitializer.minBounds.y - yPositionOffset);
        transform.localScale = new Vector2(boundariesInitializer.maxBounds.x, 1);
    }
}