using System;
using System.Collections;
using Constants;
using Managers;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;
using Zenject;

namespace CircleComponents
{
    public class CircleObjectPoolFactory : MonoBehaviour
    {
        [Inject] private BoundariesInitializerManager boundariesInitializerManager;
        [Inject] private Circle.Factory circleFactory;

        private IObjectPool<Circle> objectPool;
        private int defaultCapacity = 10;
        private int maxSize = 20;
    
        private float speedParameter;
        private float minCircleSize;
        private float maxCircleSize;
        private float timeBetweenCircleSpawns;
        private float spawnTimeVariance;
        private float minimumSpawnTime;
        private float randomSpawnTime;
        public float CircleDiameter { get; private set; }
        public Color CircleColor;

        [Inject]
        public void Construct(Settings settings)
        {
            speedParameter = settings.SpeedParameter;
            minCircleSize = settings.MinCircleSize;
            maxCircleSize = settings.MaxCircleSize;
            timeBetweenCircleSpawns = settings.TimeBetweenCircleSpawns;
            spawnTimeVariance = settings.SpawnTimeVariance;
            minimumSpawnTime = settings.MinimumSpawnTime;
        }
    
        private void Start()
        {
            objectPool = new ObjectPool<Circle>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy,
                false, defaultCapacity, maxSize);
        
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
                var circleInstance = objectPool.Get();
                InitializeCircle(circleInstance);
            
                float randomSpawnPositionX = Random.Range(boundariesInitializerManager.MinBounds.x + CircleDiameter / 2,
                    boundariesInitializerManager.MaxBounds.x - CircleDiameter / 2);

                circleInstance.transform.position =
                    new Vector2(randomSpawnPositionX, boundariesInitializerManager.MaxBounds.y + maxCircleSize);

                yield return new WaitForSeconds(GetRandomSpawnTime());
            }
        }

        public void DeactivateCircle(Circle circle)
        {
            objectPool.Release(circle);
        }

        public void IncreaseDifficulty()
        {
            timeBetweenCircleSpawns -= timeBetweenCircleSpawns / GameplayConstants.DifficultyParameter;
            spawnTimeVariance -= spawnTimeVariance / GameplayConstants.DifficultyParameter;
            minimumSpawnTime -= minimumSpawnTime / GameplayConstants.DifficultyParameter;
            speedParameter += speedParameter / GameplayConstants.DifficultyParameter;
            maxCircleSize -= maxCircleSize / GameplayConstants.DifficultyParameter;
        }
    
        private float GetRandomSpawnTime()
        {
            float spawnTime = Random.Range(timeBetweenCircleSpawns - spawnTimeVariance,
                timeBetweenCircleSpawns + spawnTimeVariance);
            return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
        }
    
        private void InitializeCircle(Circle circleInst)
        {
            circleInst.SetUpColor(CircleColor);
            CircleDiameter = Random.Range(minCircleSize, maxCircleSize);
            circleInst.transform.localScale = new Vector2(CircleDiameter, CircleDiameter);
            circleInst.SetUpSpeed(new Vector2(0, -(speedParameter / CircleDiameter)));
        }
    
        public void ChangeCirclesColor()
        {
            CircleColor = new Color(Random.value, Random.value, Random.value, 1);
        }
    
        [Serializable]
        public class Settings
        {
            public float SpeedParameter;
            public float MinCircleSize;
            public float MaxCircleSize;
            public float TimeBetweenCircleSpawns;
            public float SpawnTimeVariance;
            public float MinimumSpawnTime;
        }
    }
}