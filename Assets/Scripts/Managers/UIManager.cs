using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Constants;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private Animator levelDisplayAnimator;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI pointsText;

        private void Awake()
        {
            pauseScreen.SetActive(false);
            SetPointsText(GameplayConstants.Zero);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseScreen.activeInHierarchy)
                    PauseGame(false);
                else
                    PauseGame(true);
            }
        }
    
        public void PauseGame(bool status)
        {
            pauseScreen.SetActive(status);

            if (status)
                Time.timeScale = GameplayConstants.Zero;
            else
                Time.timeScale = GameplayConstants.One;
        }

        public void StartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = GameplayConstants.One;
        }

        public void StopGame()
        {
            Time.timeScale = GameplayConstants.Zero;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void DisplayLevel(int levelNumber)
        {
            levelText.text = $"LEVEL {levelNumber}";
            levelDisplayAnimator.SetTrigger("appear");
        }

        public void SetPointsText(int pointsValue)
        {
            pointsText.text = pointsValue.ToString("0");
        }
    }
}