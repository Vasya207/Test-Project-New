using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CircleSpawner : Singleton
{
    [Header("Circle Prefab")]
    [SerializeField] private Circle circlePrafab;
    
    [Header("Circle Parameters")]
    [SerializeField] private float speedParameter = 2;
    [SerializeField] private float minCircleSize = 0.5f;
    [SerializeField] private float maxCircleSize = 3f;
    
    [Header("Spawn Time")]
    [SerializeField] private float timeBetweenCircleSpawns = 3f;
    [SerializeField] private float spawnTimeVariance = 1f;
    [SerializeField] private float minimumSpawnTime = 1f;
    
    private float randomSpawnTime; 
    public float circleDiameter { get; private set; }
    public Color circleColor;
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
            InitializeCircle(circleInstance);
            
            float randomSpawnPositionX = Random.Range(boundariesInitializer.minBounds.x + circleDiameter / 2,
                boundariesInitializer.maxBounds.x - circleDiameter / 2);

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

    private void InitializeCircle(Circle circleInst)
    {
        circleDiameter = Random.Range(minCircleSize, maxCircleSize);
        circleInst.circleDiameter = circleDiameter;
        circleInst.transform.localScale = new Vector2(circleDiameter, circleDiameter);
        circleInst.SetUpSpeed(new Vector2(0,-(speedParameter / circleDiameter)));
        circleInst.SetUpColor(circleColor);
    }
    
    public void DeactivateCircle(Circle circle) => pool.Release(circle);

    public void IncreaseDifficulty()
    {
        timeBetweenCircleSpawns -= timeBetweenCircleSpawns / 100 * 10;
        spawnTimeVariance -= spawnTimeVariance / 100 * 10;
        minimumSpawnTime -= minimumSpawnTime / 100 * 10;
        speedParameter += speedParameter / 100 * 10;
        maxCircleSize -= maxCircleSize / 100 * 10;
    }
}
