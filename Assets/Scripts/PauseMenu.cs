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
    public bool totalRestart;

    public void Awake(){
        totalRestart = false;
        Events.OnRequestTotalRestartBool += getRestartBool;
    }

    public void OnDestroy(){
        Events.OnRequestTotalRestartBool -= getRestartBool;
    }
    

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

        isPaused = false;
        Debug.Log("Is it here");
        hasWon = false;
        totalRestart = true;
        winMenuUI.SetActive(false);
        Events.Respawn();
        totalRestart = false;
        //SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public bool getRestartBool(){
        return totalRestart;
    }

}
