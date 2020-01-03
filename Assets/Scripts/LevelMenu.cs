using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenu : MonoBehaviour
{
    public GameObject MainMenu;

    public void Back()
    {
        this.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
    }
}
