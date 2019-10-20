using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotation : MonoBehaviour
{

    public List<CinemachineVirtualCamera> PlayerCameras;
    private int current;
    

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(current == PlayerCameras.Count-1)
            {
                current = 0;
            }
            else
            {
                current++;
            }


            PlayerCameras[current].MoveToTopOfPrioritySubqueue();
            
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            if (current == 0)
            {
                current = PlayerCameras.Count - 1;
            }
            else
            {
                current--;
            }

            PlayerCameras[current].MoveToTopOfPrioritySubqueue();
            
        }



    }

    
}
