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

    private const float minCircleSize = 0.5f;
    private const float maxCircleSize = 3f;
    private float circleDiameter;
    
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
            circleDiameter = Random.Range(minCircleSize, maxCircleSize);
            float randomSpawnPositionX = Random.Range(boundariesInitializer.minBounds.x + circleDiameter / 2,
                boundariesInitializer.maxBounds.x - circleDiameter / 2);
            
            Circle circleInstance = 
                Instantiate(circlePrafab, 
                    new Vector3(randomSpawnPositionX, (float)(boundariesInitializer.maxBounds.y + 3), 0), 
                    quaternion.identity);
            
            circleInstance.SetSettings(circleDiameter);
            yield return new WaitForSeconds(GetRandomSpawnTime());
        }
    }
    private float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenCircleSpawns - spawnTimeVariance, timeBetweenCircleSpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
