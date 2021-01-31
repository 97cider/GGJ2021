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
    [SerializeField] private int maxJumps = 2;

    [SerializeField] private float m_inAirModifier = 2;

    public PlayerStats playerstats;
    const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
    [SerializeField] private bool m_Grounded = false;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    public Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public Vector3 m_Velocity = Vector3.zero;

    [SerializeField] private int availableJumps = 2;
    private float horizontalInput = 0.0f;
    private float startingGravScale = 1.0f;
    private bool jumpInput = false;

    [Header("Animations")]
    [SerializeField] private string walkAnimationName; 

    [SerializeField] private string jumpAnimationName;

    [SerializeField] private string idleAnimationName;

    [SerializeField] private string fallAnimationName;

    [SerializeField] private string attackAnimationName;

    [SerializeField] private string landAnimationName;

    [SerializeField] private Animator Animator;

    [SerializeField] private GameObject CharacterSprite;

    [SerializeField] private GameObject ShootPoint;

    [SerializeField] private PlayerEffectsController effectsController;

    [Header("Jumping")]
    [SerializeField] private float heavyLandVelocityThreshold;
    
    [SerializeField] private float jumpDelay;
    private float _currentJumpDelay;
    private bool jumpReady = false;
    private bool jumpTriggered = false;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    public UnityEvent OnHeavyLandEvent;

    public void setCCStats(float jfm, float hms, int mj, float ac){
            m_JumpForce = m_JumpForce * jfm;
            horizontalMovementSpeed =  horizontalMovementSpeed * hms;
            maxJumps = maxJumps + mj;
            m_inAirModifier = m_inAirModifier * ac;
    }
    private void Awake()
    {
        playerstats = this.GetComponent<PlayerStats>();
        playerstats.canMove = true;

        Accessory ca = playerstats.getCurrentAccessory();
        
        if(ca != null)
        {
            m_JumpForce = m_JumpForce * ca.jumpSpeedModifier;
            horizontalMovementSpeed =  horizontalMovementSpeed * ca.movementSpeedModifier;
            maxJumps = maxJumps + ca.maxJumpScalar;
        }

        //Apply movment based effects from playerstats to the character controller

        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        startingGravScale = m_Rigidbody2D.gravityScale;
        effectsController = this.GetComponent<PlayerEffectsController>();
        availableJumps = maxJumps;

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

    }

    private void Update()
    {
        if (playerstats.canMove){
            horizontalInput = Input.GetAxisRaw("Horizontal");
            jumpInput = Input.GetButtonDown("Jump");

            // Debug.Log("Horizontal input: " + horizontalInput);
            // Debug.Log("Vertical input: " + jumpInput);
            // Debug.Log("Available jumps: " + availableJumps);

            Move(horizontalInput * horizontalMovementSpeed);
            if (jumpInput && availableJumps > 0)
            {
                Animator.Play(jumpAnimationName);
                _currentJumpDelay = jumpDelay;
                jumpReady = false;
                jumpTriggered = true;
            }

            if (!jumpReady)
            {
                _currentJumpDelay -= Time.deltaTime;
                if (_currentJumpDelay <= 0) 
                {
                    jumpReady = true;
                }
            }
            if (jumpReady && jumpTriggered) 
            {
                Jump(true);
                effectsController.ShakeCameraOnJump();
                jumpReady = false;
                jumpTriggered = false;
            }
            if (horizontalInput != 0 && m_Grounded) 
            {
            Animator.Play(walkAnimationName);
            }
            if (!m_Grounded)
            {
                if(!AnimatorIsPlaying(jumpAnimationName) && !AnimatorIsPlaying(attackAnimationName) && !AnimatorIsPlaying(landAnimationName))
                {
                    Animator.Play(fallAnimationName);
                }
            }
            // if(m_Grounded && horizontalInput == 0) 
            // {
            //     if(!AnimatorIsPlaying(jumpAnimationName) && !AnimatorIsPlaying(attackAnimationName) && !AnimatorIsPlaying(landAnimationName))
            //     {
            //         Animator.Play(idleAnimationName);
            //     }
            // }
            CheckDownwardTrajectory();
        }
        
    }

    public void resetStats(){
         m_JumpForce =400f;
         horizontalMovementSpeed = .75f;
         maxJumps = 2;
    }
    bool AnimatorIsPlaying(string stateName)
    {
        return Animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
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
                
                if(m_Rigidbody2D.velocity.y > heavyLandVelocityThreshold && !wasGrounded)
                {
                    OnHeavyLandEvent.Invoke();
                }

                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                    Debug.Log("Playing a land animation");
                    Animator.Play(landAnimationName);
                }
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

            if (!m_Grounded)
            {
                targetVelocity = new Vector2(targetVelocity.x * m_inAirModifier, targetVelocity.y);
            }

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
        bool hasJumpLeft = true;

        if(availableJumps == 0)
        {
            hasJumpLeft = false;
        }

        // If the player should jump...
        if (jump && hasJumpLeft)
        {
            ResetJumpVel();
            availableJumps -= 1;
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
        m_Rigidbody2D.gravityScale = startingGravScale;
    }

    //Yea this is probably completely unneeded but I was 
    //Having fun learning how to use UnityEvents
    public void HandleLand()
    {
        availableJumps = maxJumps;
        m_Rigidbody2D.gravityScale = startingGravScale;
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0.0f);
    }

    private void CheckDownwardTrajectory()
    {
        float curVerticalVel = m_Rigidbody2D.velocity.y;

        if(!m_Grounded && curVerticalVel < 0.0f)
        {
            Debug.Log("gonna add grav scale");
            m_Rigidbody2D.gravityScale += (Time.deltaTime * addedGrav);
            m_Rigidbody2D.gravityScale = Mathf.Clamp(m_Rigidbody2D.gravityScale, startingGravScale, maxGravityScale);
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 shootPointPosition = ShootPoint.transform.localPosition;
        shootPointPosition.x *= -1;
        ShootPoint.transform.localPosition = shootPointPosition;

        Vector3 spriteScale = CharacterSprite.transform.localScale;
        spriteScale.x *= -1;
        CharacterSprite.transform.localScale = spriteScale;
    }
}
