using UnityEngine;

public class CircleCleaner : MonoBehaviour
{
    [SerializeField] private CircleSpawner circleSpawner;
    private BoundariesInitializer boundariesInitializer;
    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        boundariesInitializer = GetComponentInParent<BoundariesInitializer>();
        transform.position = new Vector2(0, boundariesInitializer.minBounds.y - 1);
        transform.localScale = new Vector2(boundariesInitializer.maxBounds.x, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Circle>() != null)
        {
            
        }
    }
}
