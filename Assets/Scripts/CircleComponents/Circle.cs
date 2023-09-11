using Signals;
using UnityEngine;
using Zenject;

namespace CircleComponents
{
    public class Circle : MonoBehaviour
    {
        [Inject] private SignalBus signalBus;

        private SpriteRenderer spriteRenderer;
        private new Rigidbody2D rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnMouseDown()
        {
            DeactivateObject();
        }

        private void DeactivateObject()
        {
            signalBus.Fire(new AddPointsSignal(transform.localScale.x));
            signalBus.Fire(new PlayParticlesSignal(spriteRenderer.color, transform.position));
            signalBus.Fire(new DeactivateCircleSignal(this));
        }

        public void SetUpSpeed(Vector2 vel)
        {
            rigidbody.velocity = vel;
        }

        public void SetUpColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public class Factory : PlaceholderFactory<Circle>
        {
        }
    }
}