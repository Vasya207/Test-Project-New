using System;
using Core;
using Signals;
using UnityEngine;
using Zenject;

public class CircleCleaner : MonoBehaviour
{
    [Inject] private BoundariesInitializerManager boundariesInitializerManager;
    [Inject] private SignalBus signalBus;

    private void Start()
    {
        SetUp();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var circle = other.GetComponent<Circle>();
        if (circle != null)
        {
            signalBus.Fire(new OnDeactivateCircleSignal(circle));
        }
    }

    private void SetUp()
    {
        transform.position = new Vector2(0, boundariesInitializerManager.minBounds.y + boundariesInitializerManager.minBounds.y);
        transform.localScale = new Vector2(boundariesInitializerManager.maxBounds.x, Constants.One);
    }
}