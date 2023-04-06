using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Circle : MonoBehaviour
{
    [SerializeField] private float speedParameter = -2;
    
    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetScaleAndSpeed(float circleDiameter)
    {
        transform.localScale = new Vector2(circleDiameter, circleDiameter);
        rigidbody.velocity = new Vector2(0,speedParameter / circleDiameter);
        spriteRenderer.color = Random.ColorHSV();
    }
}
