using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotation : MonoBehaviour
{

    //public List<CinemachineVirtualCamera> PlayerCameras;
    private int current;
    private float x;
    private float y;
    private Vector3 rotateValue;
    private bool isPressed;
    private int rotateDir;

    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        isPressed = false;
        //PlayerCameras[current].MoveToTopOfPrioritySubqueue();
    }

    // Update is called once per frame
    void Update()
    {

        if (isPressed)
        {
            
            transform.Rotate(0, 0, speed * rotateDir);


            current += 1;

            if (current == 90/speed) 
            {
                isPressed = false;
               
                current = 0;
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isPressed = true;
            rotateDir = 1;
            
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isPressed = true;
            rotateDir = -1;
        }



    }

    
}
