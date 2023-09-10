using Signals;
using UnityEngine;
using Zenject;

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
        signalBus.Fire(new OnAddPointsSignal(transform.localScale.x));
        signalBus.Fire(new OnPlayParticlesSignal(spriteRenderer.color, transform.position));
        signalBus.Fire(new OnDeactivateCircleSignal(this));
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
