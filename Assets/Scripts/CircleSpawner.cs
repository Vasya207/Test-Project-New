using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
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

    private void Awake()
    {
        boundariesInitializer = GetComponentInParent<BoundariesInitializer>();
    }

    private void Start()
    {
        StartCoroutine(SpawnAtRandomPosition());
    }

    private IEnumerator SpawnAtRandomPosition()
    {
        while (true)
        {
            Circle circleInstance = Instantiate(circlePrafab);
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
}
