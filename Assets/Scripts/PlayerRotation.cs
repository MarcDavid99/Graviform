using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Transform LeftCheck, RightCheck, MiddleCheck;

    private bool isPressed = false;
    private string keyPressed;
    Vector2 gravity;
    private int counter= 0;
    private Rigidbody2D rb;
    private int direction = 0;
    private string[] directions = { "down", "left", "up", "right" };
    private float RotateTime = 0;
    private float _rotateTimeFrequency = 0.3f;
    private bool _collision;

    //If axisDirection = 0, then when moving change x coordinates, otherwise y coordinates (if flipped to the side)s
    public int axisDirection = 0;
    public int moveDirection;
    public AudioClipGroup RotationSound;

    private void Awake()
    {
        Events.OnChangeGravity += handleRotation;
        Events.OnRequestGravityDirection += getCurrentDirection;
        Physics2D.gravity = new Vector2(0, -9.8f);
    }

    private void OnDestroy()
    {
        Events.OnChangeGravity -= handleRotation;
        Events.OnRequestGravityDirection -= getCurrentDirection;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveDirection = 1;
    }

    void Update()
    {
        if (Time.timeScale > 0 && RotateTime < Time.time) {

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Events.FacingRight())
                {
                    _collision = Physics2D.Linecast(MiddleCheck.position, LeftCheck.position, 1 << LayerMask.NameToLayer("Ground"));
                }
                else
                {
                    _collision = Physics2D.Linecast(MiddleCheck.position, RightCheck.position, 1 << LayerMask.NameToLayer("Ground"));
                }
                if (!_collision)
                {
                    RotateTime = Time.time + _rotateTimeFrequency;
                    Events.ChangeGravity(1);
                    Events.ChangeDrone(1);
                    _collision = false;
                    RotationSound.Play();
                }
                
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (Events.FacingRight())
                {
                    _collision = Physics2D.Linecast(MiddleCheck.position, RightCheck.position, 1 << LayerMask.NameToLayer("Ground"));
                }
                else
                {
                    _collision = Physics2D.Linecast(MiddleCheck.position, LeftCheck.position, 1 << LayerMask.NameToLayer("Ground"));
                }
                if (!_collision)
                {
                    RotateTime = Time.time + _rotateTimeFrequency;
                    Events.ChangeGravity(-1);
                    Events.ChangeDrone(-1);
                    _collision = false;
                    RotationSound.Play();
                }
                
            }
        }
    }

    void handleGravity(float rotation)
    {
        if (rotation == 0)
        {
            axisDirection = 0;
            moveDirection = 1;
            Physics2D.gravity = new Vector2(0, -9.81f);
        }

        else if (rotation < 0)
        {
            axisDirection = 1;
            moveDirection = -1;
            Physics2D.gravity = new Vector2(-9.81f, 0);
        }
        else if (rotation > 3)
        {
            axisDirection = 0;
            moveDirection = -1;
            Physics2D.gravity = new Vector2(0, 9.81f);
        }
        else
        {
            axisDirection = 1;
            moveDirection = 1;
            Physics2D.gravity = new Vector2(9.81f, 0);
        }
    }

    void handleRotation(int direction)
    {
        if (direction == -1)
        {
            this.direction += direction;
            if (this.direction == -1)
            {
                this.direction = 3;
            }
            transform.Rotate(0, 0, -90);
            handleGravity(transform.rotation.ToEuler().z);

        }

        if (direction == 1)
        {
            this.direction += direction;
            if (this.direction == 4)
            {
                this.direction = 0;
            }
            transform.Rotate(0, 0, 90);
            handleGravity(transform.rotation.ToEuler().z);
        }

        Events.ChangeCamera(Events.RequestGravityDirection());
    }

   string getCurrentDirection()
    {
        return directions[direction];
    }

}
