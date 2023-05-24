using Core;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private int startingPointsBarrier = 100;
    //[SerializeField] private SetUpBackground setUpBackground;

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