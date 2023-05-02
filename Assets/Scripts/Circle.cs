using UnityEngine;

public class Circle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody;
    private new ParticleSystem particleSystem;
    private ParticleSystem.MainModule settings;
    
    private PointsManager pointsManager;
    private CircleSpawner circleSpawner;
    private ParticleSystemController particleSystemController;
    
    public float circleDiameter;

    private void Awake()
    {
        particleSystemController = ParticleSystemController.Instance;
        pointsManager = PointsManager.Instance;
        circleSpawner = CircleSpawner.Instance;
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        circleSpawner.DeactivateCircle(this);
        pointsManager.AddPoints(circleDiameter);
        particleSystemController.PlayParticles(spriteRenderer, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Cleaner"))
        {
            circleSpawner.DeactivateCircle(this);
        }
    }

    public void SetUpSpeed(Vector2 vel)
    {
        rigidbody.velocity = vel;
    }

    public void SetUpColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
