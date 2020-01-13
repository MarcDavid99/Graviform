using UnityEngine;
using UnityEngine.Events;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private PlayerRotation m_PlayerRotate;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private int moveDir;
    private int axisDir;
    public AudioClipGroup DeathSound;
    public AudioClipGroup JumpSound;
    public AudioClipGroup SpawnSound;
    public AudioClipGroup CheckpointSound;

    private bool isRotating = false;
    private float rotation;

    private GameObject checkPoint;
    private float checkPointRotation;
    private GameObject Spawn;
    private Collider2D currentCheckpoint;
    private Color green = new Color(0, 255f, 0, 255f);
    private Color red = new Color(255f, 255f, 255f, 255f);

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;


    private float upDirection;
    private string whatsUp;

    private void Awake()
    {

        Events.OnRespawn += Respawn;
        Events.OnFacingRight += GetFacingRight;
        Events.OnWin += onWin;

        Spawn = GameObject.FindWithTag("Respawn");
        checkPoint = Spawn;
        checkPointRotation = Spawn.gameObject.transform.eulerAngles.z;
        currentCheckpoint = null;

        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_PlayerRotate = GetComponent<PlayerRotation>();


        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void OnDestroy()
    {
        Events.OnRespawn -= Respawn;
        Events.OnFacingRight -= GetFacingRight;
        Events.OnWin -= onWin;
    }

    private void FixedUpdate()
    {

        bool wasGrounded = m_Grounded;
        m_Grounded = false;



        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        axisDir = m_PlayerRotate.axisDirection;

        moveDir = m_PlayerRotate.moveDirection;

        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }



            if (axisDir == 0)
            {

                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(moveDir * move * 10f, m_Rigidbody2D.velocity.y);
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
            else
            {
                Vector3 targetVelocity = new Vector2(m_Rigidbody2D.velocity.x, moveDir * move * 10f);
                m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
            }
            //And then smoothing it out and applying it to the character


            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            if (axisDir == 0)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, moveDir * m_JumpForce));
            }
            else
            {
                m_Rigidbody2D.AddForce(new Vector2(-moveDir * m_JumpForce, 0f));
            }

            JumpSound.Play();

        }

    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Trap"))
        {
            DeathSound.Play();
            //Debug.Log("Spawn");
            Events.Respawn();
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Checkpoint") && checkPoint.transform.position != collision.gameObject.transform.position)
        {
            //The following should make it so that the checkpoint sound is played only once per checkpoint, so you cant walk back and forth and spam sounds.
            //Not working atm
            /*if (currentCheckpoint == null){
                Debug.Log("First time touchin");
                currentCheckpoint = collision;
            }else{
                if (collision.name.Equals(currentCheckpoint.name)){
                    Debug.Log("Is same checkpoint");
                    CheckpointSound.Mute();
                }else{
                    Debug.Log("Is different checkpoint");
                    CheckpointSound.UnMute();
                    currentCheckpoint = collision;
                }
            }*/

            checkPoint.GetComponent<SpriteRenderer>().color = red;

            Debug.Log("Is CP" + CheckpointSound.VolumeMin);
            CheckpointSound.Play();

            checkPoint = collision.gameObject;
            checkPointRotation = collision.gameObject.transform.eulerAngles.z;
            checkPoint.GetComponent<SpriteRenderer>().color = green;
        }
    }

    //When game is won, reset "checkpoint" to spawn.
    private void onWin()
    {
        checkPoint.GetComponent<SpriteRenderer>().color = red;
        checkPoint = Spawn;
        checkPointRotation = Spawn.gameObject.transform.eulerAngles.z;

    }

    private int[] arrangeGravityChangeValues(float checkPointRotation)
    {
        int[] toReturn = new int[4];

        if (checkPointRotation == 0 || checkPointRotation == 180)
        {

            toReturn[0] = 2;
            toReturn[1] = -1;
            toReturn[2] = 0;
            toReturn[3] = 1;

            //Opposite if opposite rotation
            if (checkPointRotation == 180)
            {
                toReturn[2] = 2;
                toReturn[3] = -1;
                toReturn[0] = 0;
                toReturn[1] = 1;

            }

        }
        if (checkPointRotation == 90 || checkPointRotation == 270)
        {
            Debug.Log("rotation is 90 or 270");
            //TODO FIX
            toReturn[0] = -1;
            toReturn[1] = 0;
            toReturn[2] = 1;
            toReturn[3] = 2;

            //Opposite if opposite rotation
            if (checkPointRotation == 270)
            {
                toReturn[2] = -1;
                toReturn[3] = 0;
                toReturn[0] = 1;
                toReturn[1] = 2;

            }

        }

        return toReturn;

    }

    private void Respawn()
    {
        this.GetComponent<ParticleSystem>().Play();
        SpawnSound.Play();
        this.gameObject.transform.position = checkPoint.transform.position;
        string orientation = Events.RequestGravityDirection();
        int[] listOfGravityChangeValues = arrangeGravityChangeValues(checkPointRotation);




        Events.ResetCoinCounter();

        Debug.Log("Orientation: " + orientation + "    checkpointrotation: " + checkPointRotation);

        if (orientation.Equals("up"))
        {
            int gravityValue = listOfGravityChangeValues[0];
            if (gravityValue == 2)
            {
                Events.ChangeGravity(1);
                Events.ChangeGravity(1);
            }
            else if (gravityValue != 0)
            {
                Events.ChangeGravity(gravityValue);
            }


        }
        if (orientation.Equals("left"))
        {
            int gravityValue = listOfGravityChangeValues[1];
            if (gravityValue == 2)
            {
                Events.ChangeGravity(1);
                Events.ChangeGravity(1);
            }
            else if (gravityValue != 0)
            {
                Events.ChangeGravity(gravityValue);
            }


        }
        if (orientation.Equals("down"))
        {
            int gravityValue = listOfGravityChangeValues[2];
            if (gravityValue == 2)
            {
                Events.ChangeGravity(1);
                Events.ChangeGravity(1);
            }
            else if (gravityValue != 0)
            {
                Events.ChangeGravity(gravityValue);
            }


        }
        if (orientation.Equals("right"))
        {
            int gravityValue = listOfGravityChangeValues[3];
            if (gravityValue == 2)
            {
                Events.ChangeGravity(1);
                Events.ChangeGravity(1);
            }
            else if (gravityValue != 0)
            {
                Events.ChangeGravity(gravityValue);
            }


        }


    }

    private bool GetFacingRight()
    {
        return m_FacingRight;
    }
}