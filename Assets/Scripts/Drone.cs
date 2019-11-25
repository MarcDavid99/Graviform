using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Bullet LaserPrefab;
    public float LaserSpawnFrequency;

    public int Direction;
    public float MovingSpeed;

    private Rigidbody2D _rigidbody;
    private string _gravity;
    private float _laserSpawnTime;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _gravity = Events.RequestGravityDirection();
        _laserSpawnTime = 0;
        SwitchAxis(_gravity);
        Move();
    }


    void Update()
    {

        /*if (Time.time > _laserSpawnTime)
        {
            _laserSpawnTime = Time.time + LaserSpawnFrequency;
            Bullet laser1 = GameObject.Instantiate<Bullet>(LaserPrefab, null);
            Bullet laser2 = GameObject.Instantiate<Bullet>(LaserPrefab, null);
            laser1.transform.position = this.transform.position;
            laser2.transform.position = this.transform.position;
            
            if (Direction == -1 || Direction == 1)
            {
                laser1.Direction = "left";
                laser2.Direction = "right";

            }
            else
            {
                laser1.Direction = "up";
                laser2.Direction = "down";
            }
        }*/

        // In the future gonna make it more dynamic.
        if (!Events.RequestGravityDirection().Equals(_gravity))
        {
            _gravity = Events.RequestGravityDirection();
            SwitchAxis(_gravity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Bullet"))
        {
            Direction *= -1;
            Move();
        }
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
