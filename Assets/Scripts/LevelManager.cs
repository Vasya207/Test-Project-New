using Core;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private SetUpBackground setUpBackground;
    [SerializeField] private int startingPointsBarrier = 100;

    private int currentLevel = 1;

    private void Start()
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