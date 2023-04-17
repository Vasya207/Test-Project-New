using UnityEngine;

public class Circle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody;
    private new ParticleSystem particleSystem;
    private ParticleSystem.MainModule settings;
    
    private PointsManager pointsManager;
    private CircleSpawner circleSpawner;
    
    public float circleDiameter;

    private void Awake()
    {
        particleSystem = FindObjectOfType<ParticleSystem>();
        pointsManager = FindObjectOfType<PointsManager>();
        circleSpawner = FindObjectOfType<CircleSpawner>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        settings = FindObjectOfType<ParticleSystem>().main;
    }

    private void OnMouseDown()
    {
        circleSpawner.DeactivateCircle(this);
        pointsManager.AddPoints(circleDiameter);
        PlayParticles();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Cleaner"))
        {
            circleSpawner.DeactivateCircle(this);
        }
    }

    private void PlayParticles()
    {
        particleSystem.transform.position = transform.position;
        settings.startColor = new ParticleSystem.MinMaxGradient(spriteRenderer.color);
        particleSystem.Play();
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
