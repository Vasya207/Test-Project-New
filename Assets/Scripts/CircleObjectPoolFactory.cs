using System;
using System.Collections;
using Core;
using Core.Commands;
using Signals;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;
using Zenject;

public class CircleObjectPoolFactory : MonoBehaviour
{
    [SerializeField] private float speedParameter = Constants.SpeedParameter;
    [SerializeField] private float minCircleSize = Constants.MinCircleSize;
    [SerializeField] private float maxCircleSize = Constants.MaxCircleSize;
    [SerializeField] private float timeBetweenCircleSpawns = Constants.TimeBetweenCircleSpawns;
    [SerializeField] private float spawnTimeVariance = Constants.SpawnTimeVariance;
    [SerializeField] private float minimumSpawnTime = Constants.MinimumSpawnTime;
    
    [Inject] private BoundariesInitializer boundariesInitializer;
    [Inject] private Circle.Factory circleFactory;
    
    private IObjectPool<Circle> objectPool;
    
    private float randomSpawnTime;
    public float circleDiameter { get; private set; }
    [HideInInspector] public Color circleColor;
    
    private void Start()
    {
        objectPool = new ObjectPool<Circle>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy,
            false, 10, 20);
        
        StartCoroutine(SpawnAtRandomPosition());
    }
    
    private Circle CreateFunc()
    {
        var circleInstance = circleFactory.Create();
        circleInstance.transform.SetParent(transform);
        return circleInstance;
    }
    
    private void ActionOnGet(Circle circle)
    {
        circle.gameObject.SetActive(true);
    }
    
    private void ActionOnRelease(Circle circle)
    {
        circle.gameObject.SetActive(false);
    }
    
    private void ActionOnDestroy(Circle circle)
    {
        Destroy(circle.gameObject);
    }
    
    private IEnumerator SpawnAtRandomPosition()
    {
        while (true)
        {
            Circle circleInstance = objectPool.Get();
            InitializeCircle(circleInstance);
            
            float randomSpawnPositionX = Random.Range(boundariesInitializer.minBounds.x + circleDiameter / 2,
                boundariesInitializer.maxBounds.x - circleDiameter / 2);

            circleInstance.transform.position =
                new Vector2(randomSpawnPositionX, boundariesInitializer.maxBounds.y + maxCircleSize);

            yield return new WaitForSeconds(GetRandomSpawnTime());
        }
    }

    public void DeactivateCircle(Circle circle)
    {
        objectPool.Release(circle);
    }

    public void IncreaseDifficulty()
    {
        timeBetweenCircleSpawns -= timeBetweenCircleSpawns / Constants.DifficultyParameter;
        spawnTimeVariance -= spawnTimeVariance / Constants.DifficultyParameter;
        minimumSpawnTime -= minimumSpawnTime / Constants.DifficultyParameter;
        speedParameter += speedParameter / Constants.DifficultyParameter;
        maxCircleSize -= maxCircleSize / Constants.DifficultyParameter;
    }
    
    private float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenCircleSpawns - spawnTimeVariance,
            timeBetweenCircleSpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
    
    private void InitializeCircle(Circle circleInst)
    {
        circleInst.SetUpColor(circleColor);
        circleDiameter = Random.Range(minCircleSize, maxCircleSize);
        circleInst.transform.localScale = new Vector2(circleDiameter, circleDiameter);
        circleInst.SetUpSpeed(new Vector2(0, -(speedParameter / circleDiameter)));
    }
    
    public void ChangeCirclesColor()
    {
        circleColor = new Color(Random.value, Random.value, Random.value, 1);
    }
}
