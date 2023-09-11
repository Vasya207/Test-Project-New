using UnityEngine;
using TMPro;

namespace Managers
{
    public class TimeManager : MonoBehaviour
    {
        [Header("Timer Text Component")] [SerializeField]
        private TextMeshProUGUI timerText;

        private float currentTime;

        private void Update()
        {
            currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("0.00");
        }
    }
}