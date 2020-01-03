using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject LevelMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Game has been ended");
        Application.Quit();
    }

    public void SelectLevel()
    {
        this.gameObject.SetActive(false);
        LevelMenu.gameObject.SetActive(true);
    }

}
