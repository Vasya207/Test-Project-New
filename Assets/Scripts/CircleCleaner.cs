using System;
using Core;
using UnityEngine;
using Zenject;

public class CircleCleaner : MonoBehaviour
{
    [Inject] private BoundariesInitializer boundariesInitializer;
    [Inject] private CircleObjectPoolFactory circleObjectPoolFactory;
    
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
            circleObjectPoolFactory.DeactivateCircle(circle);
        }
    }

    private void SetUp()
    {
        transform.position = new Vector2(0, boundariesInitializer.minBounds.y - Constants.MaxCircleSize);
        transform.localScale = new Vector2(boundariesInitializer.maxBounds.x, Constants.One);
    }
}