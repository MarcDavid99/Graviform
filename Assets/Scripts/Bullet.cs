using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string Direction;
    public float Speed = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Direction.Equals("up"))
        {
            this.transform.position += Vector3.up * Speed * Time.deltaTime;
        } else if (Direction.Equals("down"))
        {
            this.transform.position += Vector3.down * Speed * Time.deltaTime;
        } else if (Direction.Equals("left"))
        {
            this.transform.position += Vector3.left * Speed * Time.deltaTime;
        } else if (Direction.Equals("right"))
        {
            this.transform.position += Vector3.right * Speed * Time.deltaTime;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
           GameObject.Destroy(this.gameObject);
        }
    }
}
