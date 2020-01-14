using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{

    
    public AudioClipGroup coinSound;

    private float[] coords;
    

    private void Awake()
    {
        coords = new float[4];
        coords[0] = this.transform.position.x;
        coords[1] = this.transform.position.y;
        coords[2] = this.transform.position.z;
        coords[3] = this.transform.eulerAngles.z;
    }

    
    


    

    void OnTriggerEnter2D(Collider2D col){
        
        if (col.gameObject.tag.Equals("ForCoin"))
        {
            coinSound.Play();
            Destroy(this.gameObject);
            Events.ChangeCoinCounter(coords);
        }
        
        
        
    }

}
