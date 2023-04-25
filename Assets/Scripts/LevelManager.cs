using Core;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private PointsManager pointsManager;
    [SerializeField] private CircleSpawner circleSpawner;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SetUpBackground setUpBackground;

    [SerializeField] private int startingPointsBarrier = 100;

    private int currentLevel = 1;

    private void Start()
    {
        circleSpawner.circleColor = new Color(Random.value, Random.value, Random.value, 1);
        uiManager.DisplayLevel(currentLevel);
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
        circleSpawner.circleColor = new Color(Random.value, Random.value, Random.value, 1);
        startingPointsBarrier *= 2;
        circleSpawner.IncreaseDifficulty();
        uiManager.DisplayLevel(currentLevel);
        setUpBackground.ChangeBackground();
    }
}