
  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{

    public int Direction;
    public float MovingSpeed = 0.1f;
    public Transform SightUp, SightLeft, SightRight, SightDown;

    private int[] _directions = { -1, 2, 1, -2 };
    private Rigidbody2D _rigidbody;
    private int _multiplier;
    private bool _collision = false;

    void Start()
    {
        Events.OnChangeDrone += SwitchAxis;
        _rigidbody = GetComponent<Rigidbody2D>();
        Move();
    }

    private void Update()
    {
        Raycasting();
        if (_collision)
        {
            if (Direction < 2)
            {
                Direction += 2;
            }
            else
            {
                Direction -= 2;
            }
            Move();
            _collision = false;
        }
    }

    private void SwitchAxis(int direction)
    {
        Direction += direction;
        if (Direction > 3)
        {
            Direction = 0;
        }
        if (Direction < 0)
        {
            Direction = 3;
        }
        Move();
    }

    private void Move()
    {
        switch (_directions[Direction])
        {
            case 1:
                _rigidbody.velocity = new Vector2(MovingSpeed, 0);
                break;
            case -1:
                _rigidbody.velocity = new Vector2(-MovingSpeed, 0);
                break;
            case 2:
                _rigidbody.velocity = new Vector2(0, MovingSpeed);
                break;
            case -2:
                _rigidbody.velocity = new Vector2(0, -MovingSpeed);
                break;
        }
    }

    void Raycasting()
    {
        switch (_directions[Direction])
        {
            case -1:
                _collision = Physics2D.Linecast(this.transform.position, SightLeft.position, 1 << LayerMask.NameToLayer("Ground"));
                break;
            case 1:
                _collision = Physics2D.Linecast(this.transform.position, SightRight.position, 1 << LayerMask.NameToLayer("Ground"));
                break;
            case 2:
                _collision = Physics2D.Linecast(this.transform.position, SightUp.position, 1 << LayerMask.NameToLayer("Ground"));
                break;
            case -2:
                _collision = Physics2D.Linecast(this.transform.position, SightDown.position, 1 << LayerMask.NameToLayer("Ground"));
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Direction < 2)
        {
            Direction += 2;
        }
        else
        {
            Direction -= 2;
        }
        Move();
    }
}
