using Core;
using UnityEngine;
using TMPro;

public class PointsManager : MonoBehaviour
{
    [Header("Points Text Component")] [SerializeField]
    private TextMeshProUGUI pointsText;

    [SerializeField] private float pointsParameter = 10;

    public float points { get; private set; }

    private void Start()
    {
        pointsText.text = points.ToString();
    }

    public void AddPoints(float circleDiameter)
    {
        points += pointsParameter / circleDiameter;
        pointsText.text = points.ToString("0");
    }
}