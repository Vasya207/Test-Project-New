using Managers;
using Signals;
using UnityEngine;
using Zenject;

namespace CircleComponents
{
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
                signalBus.Fire(new DeactivateCircleSignal(circle));
            }
        }

        private void SetUp()
        {
            transform.position = new Vector2(0, boundariesInitializerManager.MinBounds.y + boundariesInitializerManager.MinBounds.y);
            transform.localScale = new Vector2(boundariesInitializerManager.MaxBounds.x, Constants.GameplayConstants.One);
        }
    }
}
