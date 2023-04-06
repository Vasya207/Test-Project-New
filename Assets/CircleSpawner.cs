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

    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float randomSpawnTime;

    private const float minCircleSize = 0.25f;
    private const float maxCircleSize = 3f;
    private float circleDiameter;
    
    private void Start()
    {
        InitializeBoundaries();
        StartCoroutine(SpawnAtRandomPosition());
    }

    private IEnumerator SpawnAtRandomPosition()
    {
        while (true)
        {
            circleDiameter = Random.Range(minCircleSize, maxCircleSize);
            float randomSpawnPositionX = Random.Range(minBounds.x + circleDiameter / 2, maxBounds.x - circleDiameter / 2);
            Circle circleInstance = 
                Instantiate(circlePrafab, 
                    new Vector3(randomSpawnPositionX, (float)(maxBounds.y + 3), 0), 
                    quaternion.identity);
            circleInstance.SetScaleAndSpeed(circleDiameter);
            yield return new WaitForSeconds(GetRandomSpawnTime());
        }
    }
    private float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenCircleSpawns - spawnTimeVariance, timeBetweenCircleSpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
    
    private void InitializeBoundaries()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
}
