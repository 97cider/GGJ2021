using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
    [SerializeField] private float horizontalMovementSpeed = 40.0f;
    [SerializeField] private float maxGravityScale = 9.0f;
    [SerializeField] private float addedGrav = 0.1f;                            // Grav scale units / sec

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;

    private float horizontalInput = 0.0f;
    private float startingGravScale = 1.0f;
    private bool jumpInput = false;
    

    [SerializeField] private bool spentDoubleJump = false;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        startingGravScale = m_Rigidbody2D.gravityScale;

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpInput = Input.GetButtonDown("Jump");

        Debug.Log("Horizontal input: " + horizontalInput);
        Debug.Log("Vertical input: " + jumpInput);

        Move(horizontalInput * horizontalMovementSpeed);
        Jump(jumpInput);
        CheckDownwardTrajectory();
        
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
                
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

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
    }

    public void Jump(bool jump)
    {
        // If the player should jump...
        if (jump && (m_Grounded || !spentDoubleJump))
        {
            if(m_Grounded)
            {
                // Add a vertical force to the player.
                m_Grounded = false;
            }
            else
            {
                ResetJumpVel();
                spentDoubleJump = true;     //Maybe later add events for when we jump... sounds like a useful thing to know
            }

            ApplyJumpForce();
        }
    }

    private void ApplyJumpForce()
    {
        m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
    }

    private void ResetJumpVel()
    {
        //We'll need to reset some variables here... namely the weird grav scale changing we're doing for downward trajectories on jumping...
        //For now, just reset the up/down velocity to 0 and add a force again
        Vector2 newVelVec = m_Rigidbody2D.velocity;
        newVelVec.y = 0.0f;
        m_Rigidbody2D.velocity = newVelVec;
    }

    //Yea this is probably completely unneeded but I was 
    //Having fun learning how to use UnityEvents
    public void HandleLand()
    {
        m_Grounded = true;
        spentDoubleJump = false;
        m_Rigidbody2D.gravityScale = startingGravScale;
    }

    private void CheckDownwardTrajectory()
    {
        float curVerticalVel = m_Rigidbody2D.velocity.y;

        if(curVerticalVel < 0.0f)
        {
            m_Rigidbody2D.gravityScale += (Time.deltaTime * addedGrav);
            m_Rigidbody2D.gravityScale = Mathf.Clamp(m_Rigidbody2D.gravityScale, startingGravScale, maxGravityScale);
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
}
