using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject winMenuUI;

    [SerializeField] private bool isPaused;
    [SerializeField] public bool hasWon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }

        if (hasWon)
        {
            winMenuUI.SetActive(true);
        }
        else
        {
            winMenuUI.SetActive(false);
        }

    }
    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
    }
    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Debug.Log("Game has been ended");
        Application.Quit();
    }
    public void Restart()
    {
        hasWon = false;
        winMenuUI.SetActive(false);
        Events.Respawn();
        //SceneManager.LoadScene(1);
    }
}
