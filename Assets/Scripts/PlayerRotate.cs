using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{

    private bool isPressed = false;
    private string keyPressed;
    Vector2 gravity;
    private int counter= 0;
    private Rigidbody2D rb;

    //If axisDirection = 0, then when moving change x coordinates, otherwise y coordinates (if flipped to the side)s
    public int axisDirection = 0;
    public int moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isPressed = true;
            keyPressed = "E";

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isPressed = true;
            keyPressed = "Q";
        }

        if (isPressed)
        {
            handleRotation();
        }


    }

    void handleGravity(float rotation)
    {
        if (rotation == 0)
        {
            axisDirection = 0;
            moveDirection = 1;
            Physics2D.gravity = new Vector2(0, -9.81f);
        }

        else if (rotation < 0)
        {
            axisDirection = 1;
            moveDirection = -1;
            Physics2D.gravity = new Vector2(-9.81f, 0);
        }
        else if (rotation > 3)
        {
            axisDirection = 0;
            moveDirection = -1;
            Physics2D.gravity = new Vector2(0, 9.81f);
        }
        else
        {
            axisDirection = 1;
            moveDirection = 1;
            Physics2D.gravity = new Vector2(9.81f, 0);
        }
    }

    void handleRotation()
    {
        if (keyPressed.Equals("Q"))
        {
            Debug.Log("handleRotation method, entered Q");
            transform.Rotate(0, 0, -90);
            isPressed = false;

            handleGravity(transform.rotation.ToEuler().z);

        }

        if (keyPressed.Equals("E"))
        {
            Debug.Log("handleRotation method, entered E");
            transform.Rotate(0, 0, 90);
            isPressed = false;

            handleGravity(transform.rotation.ToEuler().z);
        }
    }

}
