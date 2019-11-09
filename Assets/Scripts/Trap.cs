using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Trap : MonoBehaviour
{
    
    public Text score;
    private int score_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void update_score()
    {
        if(score.text == "Deaths: 0")
        {
            score_count = 0;
        }
        score_count += 1;
        score.text = "Deaths: " + score_count;
    }

    public void update_time()
    {
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        update_score();
    }
}
