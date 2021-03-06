﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    private GameObject Spawn;
    //public Text score;
    //private int score_count;
    public PauseMenu pauseMenu;
    public AudioClipGroup WinSound;

    void Start()
    {
        Spawn = GameObject.FindWithTag("Respawn");
        
    }

    public void update_score()
    {
        //score_count = 0;
        //score.text = "Deaths: " + score_count;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //update_score();
        WinSound.Play();
        pauseMenu.hasWon = true;
        pauseMenu.totalRestart = true;
        Events.DoSomething();
        //collision.gameObject.transform.position = Spawn.transform.position;
    }
}
