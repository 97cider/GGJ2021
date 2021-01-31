using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingAI : MonoBehaviour
{
    [SerializeField] protected GameObject player;

    protected enum State { Idle, MoveTowards, Attack}

    protected State aiState;
    protected bool inAction;
    protected bool canMove;

    protected Vector2 direction;

    [SerializeField] private float maxDistanceToPlayer;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float idleTime;
    [SerializeField] private float moveTime;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aiState = State.Idle;
        inAction = false;
        direction = new Vector2(1, 0);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("T"))
        {
            CanMove(!canMove);
            Debug.Log("changing can move");
        }

        if(!inAction && canMove)
        {
            DecideNewAction();
            inAction = true;
            StartCorrectAction();
        }

        Debug.DrawRay(this.transform.position, direction * 30, Color.green);
    }

    private void DecideNewAction()
    {
        State oldState = aiState;

        bool closeToPlayer = false;

        if (IsCloseToPlayer())
        {
            closeToPlayer = true;
        }

        switch (oldState)
        {
            case State.Idle:
            {
                if(closeToPlayer)
                {
                    aiState = State.Attack;
                }
                else
                {
                    aiState = State.MoveTowards;
                }
                break;
            }
            case State.Attack:
            {
                //Go back to idle after we're done. 
                aiState = State.Idle;
                break;
            }
            case State.MoveTowards:
            {
                if (closeToPlayer)
                {
                    aiState = State.Attack;
                }
                else
                {
                    aiState = State.MoveTowards;
                }
                //Randomly choose attack or idle if close enough. If not, move closer again. 
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
            case State.Attack:
                {
                    StartCoroutine("Attack");
                    break;
                }
            case State.MoveTowards:
                {
                    StartCoroutine("MoveTowardsPlayer", moveTime);
                    break;
                }
        }
    }

    IEnumerator MoveTowardsPlayer(float moveTime)
    {
        float timeElapsed = 0;
        bool firstInvocation = true;

        while (true)
        {
            if(IsCloseToPlayer())
            {
                inAction = false;
                yield break;
            }

            if(!firstInvocation)
            {
                timeElapsed += Time.deltaTime;
            }
            else
            {
                firstInvocation = false;
            }

            if (timeElapsed >= moveTime)
            {
                inAction = false;
                yield break;
            }


            MoveTowards();
            yield return null;       
        }
    }

    IEnumerator Idle(float idleTime)
    {
        yield return new WaitForSeconds(idleTime);
        inAction = false;
    }

    protected abstract IEnumerator Attack();

    protected virtual void MoveTowards()
    {
        direction.x = GetXDirectionTowardsPlayer();

        //transform.LookAt(towards);  //Might need to change worldUp here???
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * enemySpeed);
    }

    protected float GetXDirectionTowardsPlayer()
    {
        Vector2 toTarget = player.transform.position - transform.position;

        if (toTarget.x > 0.0f)
        {
            return 1.0f;
        }
        else
        {
            return -1.0f;
        }
    }

    protected bool IsCloseToPlayer()
    {
        bool closeToPlayer = false;

        if (Vector2.Distance(transform.position, player.transform.position) < maxDistanceToPlayer)
        {
            closeToPlayer = true;
        }

        return closeToPlayer;
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
}
