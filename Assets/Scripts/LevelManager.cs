using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private CircleSpawner circleSpawner;
    
    [SerializeField] private int startingPointsBarrier = 100;

    private void Start()
    {
        circleSpawner.circleColor = new Color(Random.value, Random.value, Random.value, 1);
    }

    private void Update()
    {
        if (pointsManager.points >= startingPointsBarrier)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        circleSpawner.circleColor = new Color(Random.value, Random.value, Random.value, 1);
        startingPointsBarrier *= 2;
        circleSpawner.NextLevel();
    }
}
