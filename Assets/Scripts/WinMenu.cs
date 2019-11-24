using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void Restart()
    {
        
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Debug.Log("Game has been ended");
        Application.Quit();
    }


}
