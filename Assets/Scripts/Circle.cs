using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Circle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody;
    public float circleDiameter;
    
    private PointsManager pointsManager;
    private CircleSpawner circleSpawner;

    private void Awake()
    {
        pointsManager = FindObjectOfType<PointsManager>();
        circleSpawner = FindObjectOfType<CircleSpawner>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        circleSpawner.DeactivateCircle(this);
        pointsManager.AddPoints(circleDiameter);
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
