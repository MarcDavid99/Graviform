using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotation : MonoBehaviour
{

    //public List<CinemachineVirtualCamera> PlayerCameras;
    private int current;
    private Vector3 target;
    private float x;
    private float y;
    private Vector3 rotateValue;
    private bool isPressed;
    private int rotateDir;

    public float speed = 2f;
    
    public AudioClipGroup RotateSound;

    private void Awake()
    {
        Events.OnChangeCamera += RotateCamera;
    }

    private void OnDestroy()
    {
        Events.OnChangeCamera -= RotateCamera;
    }
    void Start()
    {
        current = 0;
        isPressed = false;
        //PlayerCameras[current].MoveToTopOfPrioritySubqueue();
    }

    void Update()
    {

        if (isPressed)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(target), speed * Time.deltaTime);
        }

    }

    void RotateCamera(string dir)
    {
        
        if (dir.Equals("down"))
        {
            target = new Vector3(0, 0, 0);
        }
        else if (dir.Equals("up"))
        {
            target = new Vector3(0, 0, 180);
        }
        else if (dir.Equals("left"))
        {
            target = new Vector3(0, 0, 90);
        }
        else if (dir.Equals("right"))
        {
            target = new Vector3(0, 0, -90);
        }
        RotateSound.Play();
        isPressed = true;
    }


}
