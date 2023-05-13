using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CircleSpawner : Singleton<CircleSpawner>
{
    [Header("Circle Prefab")] [SerializeField]
    private Circle circlePrafab;

    [Header("Circle Parameters")] [SerializeField]
    private float speedParameter = Constants.SpeedParameter;

    [SerializeField] private float minCircleSize = Constants.MinCircleSize;
    [SerializeField] private float maxCircleSize = Constants.MaxCircleSize;

    [Header("Spawn Time")] [SerializeField]
    private float timeBetweenCircleSpawns = Constants.TimeBetweenCircleSpawns;

    [SerializeField] private float spawnTimeVariance = Constants.SpawnTimeVariance;
    [SerializeField] private float minimumSpawnTime = Constants.MinimumSpawnTime;

    private float randomSpawnTime;
    public float circleDiameter { get; private set; }
    public Color circleColor;
    private BoundariesInitializer boundariesInitializer;
    private ObjectPool<Circle> pool;

    private void Awake()
    {
        boundariesInitializer = GetComponentInParent<BoundariesInitializer>();
    }

    private void OnEnable()
    {
        Signals.OnNewLevel.AddListener(OnNewLevel);
        Signals.OnLevelStart.AddListener(OnLevelStart);
    }

    private void OnLevelStart(int arg0)
    {
        ChangeCirclesColor();
    }

    private void OnDisable()
    {
        Signals.OnNewLevel.RemoveListener(OnNewLevel);
    }

    private void OnNewLevel(int arg0)
    {
        IncreaseDifficulty();
        ChangeCirclesColor();
    }

    private void ChangeCirclesColor()
    {
        circleColor = new Color(Random.value, Random.value, Random.value, 1);
    }
    private void Start()
    {
        pool = new ObjectPool<Circle>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy,
            false, 10, 20);

        StartCoroutine(SpawnAtRandomPosition());
    }

    private Circle CreateFunc()
    {
        return Instantiate(circlePrafab);
    }

    private void ActionOnGet(Circle shape)
    {
        shape.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Circle shape)
    {
        shape.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Circle shape)
    {
        Destroy(shape.gameObject);
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
        float spawnTime = Random.Range(timeBetweenCircleSpawns - spawnTimeVariance,
            timeBetweenCircleSpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }

    private void InitializeCircle(Circle circleInst)
    {
        circleDiameter = Random.Range(minCircleSize, maxCircleSize);
        circleInst.circleDiameter = circleDiameter;
        circleInst.transform.localScale = new Vector2(circleDiameter, circleDiameter);
        circleInst.SetUpSpeed(new Vector2(0, -(speedParameter / circleDiameter)));
        circleInst.SetUpColor(circleColor);
    }

    public void DeactivateCircle(Circle circle) => pool.Release(circle);

    private void IncreaseDifficulty()
    {
        timeBetweenCircleSpawns -= timeBetweenCircleSpawns / 100 * 10;
        spawnTimeVariance -= spawnTimeVariance / 100 * 10;
        minimumSpawnTime -= minimumSpawnTime / 100 * 10;
        speedParameter += speedParameter / 100 * 10;
        maxCircleSize -= maxCircleSize / 100 * 10;
    }
}