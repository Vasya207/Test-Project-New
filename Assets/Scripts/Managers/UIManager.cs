using System;
using Core;
using Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Animator levelDisplayAnimator;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI pointsText;
    
    //private Animator levelDisplayAnimator;

    private void Awake()
    {
        pauseScreen.SetActive(false);
        // levelDisplayAnimator = levelDisplayAnimator.GetComponent<Animator>();
        SetPointsText(Constants.Zero);
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
            Time.timeScale = Constants.Zero;
        else
            Time.timeScale = Constants.One;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = Constants.One;
    }

    public void StopGame()
    {
        Time.timeScale = Constants.Zero;
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