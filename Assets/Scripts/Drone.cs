using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public int Direction;
    public float MovingSpeed;
    private Rigidbody2D _rigidbody;
    private string _gravity;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _gravity = Events.RequestGravityDirection();
        SwitchAxis(_gravity);
        Move();
    }

    
    void Update()
    {
        // In the future gonna make it more dynamic.
        if (!Events.RequestGravityDirection().Equals(_gravity))
        {
            _gravity = Events.RequestGravityDirection();
            SwitchAxis(_gravity);
        }     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Switch: " + Direction);
        Direction *= -1;
        Debug.Log("After: " + Direction);
        Move();
    }

    private void SwitchAxis(string gravity)
    {
        if (gravity.Equals("up"))
        {
            Direction = 1;
        }
        else if (gravity.Equals("down"))
        {
            Direction = -1;
        }
        else if (gravity.Equals("left"))
        {
            Direction = -2;
        }
        else
        {
            Direction = 2;
        }
        Move();
    }

    private void Move()
    {
        switch (Direction)
        {
            case -1:
                _rigidbody.velocity = new Vector2(0, 0);
                _rigidbody.velocity = new Vector2(-MovingSpeed, GetComponent<Rigidbody2D>().velocity.y);
                break;
            case 1:
                _rigidbody.velocity = new Vector2(0, 0);
                _rigidbody.velocity = new Vector2(MovingSpeed, GetComponent<Rigidbody2D>().velocity.y);
                break;
            case 2:
                _rigidbody.velocity = new Vector2(0, 0);
                _rigidbody.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, MovingSpeed);
                break;
            case -2:
                _rigidbody.velocity = new Vector2(0, 0);
                _rigidbody.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -MovingSpeed);
                break;
        }
    }
}
