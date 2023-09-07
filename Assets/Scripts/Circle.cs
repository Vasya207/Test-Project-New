using System.Collections;
using UnityEngine;
using Zenject;

public class Circle : MonoBehaviour
{
    [Inject] private ParticleSystemController particleSystemController;
    [Inject] private PointsManager pointsManager;
    [Inject] private CircleFactory circleFactory;
    //[Inject] private CircleSpawner circleSpawner;
    
    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody;
    
    private void Awake()
    {
        gameObject.SetActive(true);
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnMouseDown()
    {
        DeactivateObject();
    }

    private void DeactivateObject()
    {
        pointsManager.AddPoints(circleFactory.circleDiameter);
        particleSystemController.PlayParticles(spriteRenderer, transform.position);
        circleFactory.DeactivateCircle(this);
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
