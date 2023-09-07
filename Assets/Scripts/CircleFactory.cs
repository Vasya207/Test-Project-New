using System.Collections;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;
using Zenject;

public class CircleFactory : MonoBehaviour
{
    [SerializeField] private float speedParameter = Constants.SpeedParameter;
    [SerializeField] private float minCircleSize = Constants.MinCircleSize;
    [SerializeField] private float maxCircleSize = Constants.MaxCircleSize;
    [SerializeField] private float timeBetweenCircleSpawns = Constants.TimeBetweenCircleSpawns;
    [SerializeField] private float spawnTimeVariance = Constants.SpawnTimeVariance;
    [SerializeField] private float minimumSpawnTime = Constants.MinimumSpawnTime;
    
    [Inject] private BoundariesInitializer boundariesInitializer;
    [Inject] private Circle.Factory circleFactory;
    
    private float randomSpawnTime;
    public float circleDiameter { get; private set; }
    [HideInInspector] public Color circleColor;
    
    private void OnEnable()
    {
        Signals.OnNewLevel.AddListener(OnNewLevel);
        Signals.OnLevelStart.AddListener(OnLevelStart);
    }

    private void OnDisable()
    {
        Signals.OnNewLevel.RemoveListener(OnNewLevel);
        //Signals.OnLevelStart.RemoveListener(OnLevelStart);
    }
    
    private void Start()
    {
        StartCoroutine(SpawnAtRandomPosition());
    }
    
    private IEnumerator SpawnAtRandomPosition()
    {
        while (true)
        {
            Circle circleInstance = circleFactory.Create();
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
        circle.gameObject.SetActive(false);
    }

    private void IncreaseDifficulty()
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
    
    public void Spawn()
    {
        Circle circleInstance = circleFactory.Create();
        circleInstance.transform.position = Vector3.zero;
    }
    
    private void OnLevelStart(int arg0)
    {
        ChangeCirclesColor();
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
}
