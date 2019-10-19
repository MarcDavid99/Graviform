using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject Spawn;

    // Start is called before the first frame update
    void Start()
    {
        Spawn = GameObject.FindWithTag("Respawn");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = Spawn.transform.position;
    }
}
