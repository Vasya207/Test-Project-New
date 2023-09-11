using UnityEngine;
using Zenject;

namespace Managers
{
    public class PointsManager : MonoBehaviour
    {
        [Inject] private UIManager uiManager;
        [SerializeField] private float pointsParameter = 10;
    
        public float Points { get; private set; }

        public void AddPoints(float circleDiameter)
        {
            Points += pointsParameter / circleDiameter;
            uiManager.SetPointsText((int)Points);
        }
    }
}