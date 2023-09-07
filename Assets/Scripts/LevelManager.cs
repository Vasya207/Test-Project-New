using System;
using Core;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int startingPointsBarrier = 100;
    [Inject] private PointsManager pointsManager;

    private int currentLevel = 1;

    private void Awake()
    {
        Signals.OnLevelStart.Invoke(currentLevel);
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
        currentLevel++;
        Signals.OnNewLevel.Invoke(currentLevel);
        startingPointsBarrier *= Constants.PointsBarrierMultiplier;
    }
}