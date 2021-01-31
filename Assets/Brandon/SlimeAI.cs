using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAI : Enemy
{
    private Rigidbody2D rb;

    private enum State { Idle, Bounce }
    private State aiState;

    [SerializeField] protected GameObject player;

    protected bool inAction;
    protected bool canMove;
    [SerializeField] protected bool m_Grounded;

    protected Vector2 direction;

    [SerializeField] private float jumpForce;
    [SerializeField] private float idleTime;

    [SerializeField] private LayerMask m_WhatIsGround;      //yea i ripped this from the char controller
    [SerializeField] private Transform m_GroundCheck;

    [SerializeField] float k_GroundedRadius;

    [SerializeField] protected GameObject deathObject;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        aiState = State.Idle;
        inAction = false;
        canMove = true;
        direction = new Vector2(1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inAction && canMove)
        {
            DecideNewAction();
            inAction = true;
            StartCorrectAction();
        }

        Debug.DrawRay(this.transform.position, direction * 30, Color.green);
    }

    void FixedUpdate()
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
                {
                    //OnLandEvent.Invoke();
                    //Debug.Log("Playing a land animation");
                    //Animator.Play(landAnimationName);
                    this.rb.velocity = Vector2.zero;
                }
            }
        }
    }

    private void DecideNewAction()
    {
        State oldState = aiState;

        switch (oldState)
        {
            case State.Idle:
                {
                    if(!m_Grounded)
                    {
                        aiState = State.Idle;
                    }
                    else
                    {
                        aiState = State.Bounce;
                    }
                    break;
                }

            case State.Bounce:
                {
                    aiState = State.Idle;
                    break;
                }
        }
        Debug.Log("Switching to state: " + aiState);
    }

    private void StartCorrectAction()
    {
        switch (aiState)
        {
            case State.Idle:
                {
                    StartCoroutine("Idle", idleTime);
                    break;
                }

            case State.Bounce:
                {
                    StartCoroutine("BounceTowardsPlayer");
                    break;
                }
        }
    }

    IEnumerator BounceTowardsPlayer()
    {
        Bounce(new Vector2(GetXDirectionTowardsPlayer(), 0.0f));

        //These two loops wait until the slime's bounce is completed
        while (m_Grounded)
        {
            Debug.Log("Waiting to be ungrounded");
            yield return null;
        }
        while (!m_Grounded)
        {
            Debug.Log("Waiting to land");
            yield return null;
        }
        inAction = false;
    }

    IEnumerator Idle(float idleTime)
    {
        yield return new WaitForSeconds(idleTime);
        inAction = false;
    }

    protected virtual void Bounce(Vector2 dir)
    {
        rb.AddForce(new Vector2(dir.x * jumpForce, jumpForce));
    }

    protected float GetXDirectionTowardsPlayer()
    {
        Vector2 toTarget = player.transform.position - transform.position;

        if (toTarget.x > 0.0f)
        {
            return  1.0f;
        }
        else
        {
            return -1.0f;
        }
    }

    public void CanMove(bool moveThatGearUp)
    {
        canMove = moveThatGearUp;
        if (!canMove)
        {
            StopAllCoroutines();
            inAction = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            if(player != null)
            {
                player.TakeDamage(this._enemyContactDamage);
            }
        }
    }

    public override void Die()
    {
        StopAllCoroutines();
        Instantiate(deathObject, this.transform.position, Quaternion.identity);
        base.Die();
        Destroy(this.gameObject);
    }
}
