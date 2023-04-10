using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CircleSpawner : MonoBehaviour
{
    [Header("Circle Prefab")]
    [SerializeField] private Circle circlePrafab;
    
    [Header("Spawn Time")]
    [SerializeField] private float timeBetweenCircleSpawns = 3f;
    [SerializeField] private float spawnTimeVariance = 1f;
    [SerializeField] private float minimumSpawnTime = 1f;
    private float randomSpawnTime;

    private BoundariesInitializer boundariesInitializer;
    private ObjectPool<Circle> pool;

    private void Awake()
    {
        boundariesInitializer = GetComponentInParent<BoundariesInitializer>();
    }

    private void Start()
    {
        pool = new ObjectPool<Circle>(() =>
        {
            return Instantiate(circlePrafab);
        }, shape =>
        {
            shape.gameObject.SetActive(true);
        }, shape =>
        {
            shape.gameObject.SetActive(false);
        }, shape =>
        {
            Destroy(shape.gameObject);
        }, false, 10, 20);
        
        StartCoroutine(SpawnAtRandomPosition());
    }

    private IEnumerator SpawnAtRandomPosition()
    {
        while (true)
        {
            Circle circleInstance = pool.Get();
            circleInstance.InitializeCircle();
            
            float randomSpawnPositionX = Random.Range(boundariesInitializer.minBounds.x + circleInstance.circleDiameter / 2,
                boundariesInitializer.maxBounds.x - circleInstance.circleDiameter / 2);

            circleInstance.transform.position =
                new Vector2(randomSpawnPositionX, boundariesInitializer.maxBounds.y + 3);
            
            yield return new WaitForSeconds(GetRandomSpawnTime());
        }
    }
    private float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenCircleSpawns - spawnTimeVariance, timeBetweenCircleSpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }

    public void DeactivateCircle(Circle circle) => pool.Release(circle);
}
