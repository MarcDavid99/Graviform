using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public RawImage testImage;
    private bool imgOn;

    // Use this for initialization
    void Start()
    {
        testImage.enabled = false;
        imgOn = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (imgOn == true)
            {
                testImage.enabled = false;
                imgOn = false;
            }

            else
            {
                testImage.enabled = true;
                imgOn = true;
            }
        }

    }
}
