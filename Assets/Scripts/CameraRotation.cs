using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotation : MonoBehaviour
{

    //public List<CinemachineVirtualCamera> PlayerCameras;
    private float current;
    private float x;
    private float y;
    private Vector3 rotateValue;
    private bool isPressed;
    private int rotateDir;

    public float speed = 2f;
    // Start is called before the first frame update

    private void Awake()
    {
        Events.OnChangeCamera += RotateCamera;
    }
    void Start()
    {
        current = 0;
        isPressed = false;
        //PlayerCameras[current].MoveToTopOfPrioritySubqueue();
    }

    // Update is called once per frame
    void Update()
    {
        if (current != 0)
        {
            Debug.Log(current);
        }

        if (isPressed)
        {

            transform.Rotate(0, 0, speed * rotateDir);


            current += rotateDir;

            if (current == 90 / speed && rotateDir == 1 || current == -90 / speed && rotateDir == -1)
            {
                isPressed = false;

                current = 0;
            }

        }

    }

    void RotateCamera(int dir)
    {
        if (isPressed && dir == rotateDir)
        {
            current -= 90 / speed * dir;
        }
        isPressed = true;
        rotateDir = dir;
    }


}
