using System;
using Core;
using Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject levelDisplay;
    [SerializeField] private TextMeshProUGUI levelText;
    
    private Animator levelDisplayAnimator;

    private void Awake()
    {
        pauseScreen.SetActive(false);
        levelDisplayAnimator = levelDisplay.GetComponent<Animator>();
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
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void StopGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DisplayLevel(int levelNumber)
    {
        levelText.text = $"LEVEL {levelNumber}";
        levelDisplayAnimator.SetTrigger("appear");
    }
}