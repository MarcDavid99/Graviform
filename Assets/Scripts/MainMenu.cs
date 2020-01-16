using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuu;
    [SerializeField] private GameObject LevelMenu;
    public AudioClipGroup ClickSound;

    public void PlayGame(int level)
    {
        SceneManager.LoadScene(level + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Game has been ended");
        Application.Quit();
    }

    public void SelectLevel()
    {
        MainMenuu.gameObject.SetActive(false);
        LevelMenu.gameObject.SetActive(true);
    }

    public void PlaySound()
    {
        Debug.Log("CLiekd");
        ClickSound.Play();
    }

    public void Back()
    {
        LevelMenu.gameObject.SetActive(false);
        MainMenuu.gameObject.SetActive(true);
    }
}
