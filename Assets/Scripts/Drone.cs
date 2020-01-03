using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public Bullet LaserPrefab;
    public float LaserSpawnFrequency;

    public int Direction;
    public float MovingSpeed = 0.1f;

    private CircleCollider2D _collider;
    private Rigidbody2D _rigidbody;
    private string _gravity;
    private float _laserSpawnTime;
    private float inverseMoveTime;
    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _gravity = Events.RequestGravityDirection();
        _laserSpawnTime = 0;
        inverseMoveTime = 1f / MovingSpeed;
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
            _rigidbody.velocity = Vector3.zero;
            _gravity = Events.RequestGravityDirection();
            SwitchAxis(_gravity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.tag.Equals("Bullet"))
        {
            _rigidbody.velocity = Vector3.zero;
            Direction *= -1;
            Move();
        }
    }

    private void SwitchAxis(string gravity)
    {
        if (gravity.Equals("up"))
        {
            if (Direction == -2)
            {
                Direction = -1;
            }
            else
            {
                Direction = 1;
            }
        }
        else if (gravity.Equals("down"))
        {
            if (Direction == -2)
            {
                Direction = -1;
            } else
            {
                Direction = 1;
            }
        }
        else if (gravity.Equals("left"))
        {
            if (Direction == -1)
            {
                Direction = -2;
            } else
            {
                Direction = 1;
            }
        }
        else
        {
            if (Direction == -1)
            {
                Direction = -2;
            }
            else
            {
                Direction = 2;
            }
        }
        Move();
    }

    private void Move()
    {
        switch (Direction)
        {
            case -1:
                _rigidbody.velocity = new Vector2(-MovingSpeed, GetComponent<Rigidbody2D>().velocity.y);
                break;
            case 1:
                _rigidbody.velocity = new Vector2(MovingSpeed, GetComponent<Rigidbody2D>().velocity.y);
                break;
            case 2:
                _rigidbody.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, MovingSpeed);
                break;
            case -2:
                _rigidbody.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -MovingSpeed);
                break;
        }
    }
}
