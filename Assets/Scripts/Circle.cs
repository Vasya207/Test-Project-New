using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Circle : MonoBehaviour
{
    [SerializeField] private float speedParameter = 2;
    [SerializeField] private float minCircleSize = 0.5f;
    [SerializeField] private float maxCircleSize = 3f;
    public float circleDiameter { get; private set; }

    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private PointsManager pointsManager;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pointsManager = FindObjectOfType<PointsManager>();
    }

    public void InitializeCircle()
    {
        circleDiameter = Random.Range(minCircleSize, maxCircleSize);
        transform.localScale = new Vector2(circleDiameter, circleDiameter);
        rigidbody.velocity = new Vector2(0,-(speedParameter / circleDiameter));
        spriteRenderer.color = new Color(Random.value, Random.value, Random.value, 1);
    }
    
    private void OnMouseDown()
    {
        Destroy(gameObject);
        pointsManager.AddPoints(circleDiameter);
    }
}
