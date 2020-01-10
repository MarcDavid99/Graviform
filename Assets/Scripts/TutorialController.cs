using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    
    [SerializeField]
    Canvas messageCanvas;
 
    void Start()
    {
        messageCanvas.enabled = false;
    }
 
    void OnTriggerEnter2D(Collider2D other)
    {
        
        messageCanvas.enabled = true;
        
    }
 
         
    void OnTriggerExit2D(Collider2D other)
    {
        
        messageCanvas.enabled = false;
        
    }
 
    
}
