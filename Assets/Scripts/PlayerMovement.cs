﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController2D controller;
    public float speed = 40f;
    public Animator animator;


    private float horizontalMove = 0f;
    private bool jump = false;
    private bool canMove = true;
    void Start()
    {

    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            horizontalMove = Input.GetAxis("Horizontal") * speed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
        }




    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    private void FixedUpdate()
    {

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

    }



}
