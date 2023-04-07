using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Circle : MonoBehaviour
{
    [SerializeField] private float speedParameter = 2;
    //[SerializeField] private float minCircleSize = 0.5f;
    //[SerializeField] private float maxCircleSize = 3f;
    
    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private PointsManager pointsManager;
    private float circleDiameter;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pointsManager = FindObjectOfType<PointsManager>();
    }

    public void SetSettings(float circleDiameter)
    {
        transform.localScale = new Vector2(circleDiameter, circleDiameter);
        rigidbody.velocity = new Vector2(0,-(speedParameter / circleDiameter));
        spriteRenderer.color = new Color(Random.value, Random.value, Random.value, 1);
        this.circleDiameter = circleDiameter;
    }
    
    private void OnMouseDown()
    {
        Destroy(gameObject);
        pointsManager.AddPoints(circleDiameter);
    }
}
