using System;
using Core;
using Signals;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int startingPointsBarrier = 100;
    
    [Inject] private PointsManager pointsManager;
    [Inject] private SignalBus signalBus;

    private int currentLevel = 1;

    private void Awake()
    {
        signalBus.Fire(new OnLevelStartSignal());
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
        signalBus.Fire(new OnNewLevelSignal(currentLevel));
        startingPointsBarrier *= Constants.PointsBarrierMultiplier;
    }
}