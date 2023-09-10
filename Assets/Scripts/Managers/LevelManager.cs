using System;
using Core;
using Signals;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [Inject] private PointsManager pointsManager;
    [Inject] private SignalBus signalBus;

    private int startingPointsBarrier;
    private int currentLevel = 1;

    [Inject]
    private void Construct(Settings settings)
    {
        startingPointsBarrier = settings.StartingPointsBarrier;
    }

    private void Start()
    {
        signalBus.Fire(new OnLevelStartSignal(currentLevel));
    }

    private void Update()
    {
        if (pointsManager.Points >= startingPointsBarrier)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        currentLevel++;
        signalBus.Fire(new OnNewLevelSignal(currentLevel));
        startingPointsBarrier *= Constants.PointsBarrierMultiplier;
    }
    
    [Serializable]
    public class Settings
    {
        public int StartingPointsBarrier = 100;
    }
}