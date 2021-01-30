using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingAI : MonoBehaviour
{
    [SerializeField] protected GameObject player;

    protected enum State { Idle, MoveTowards, Attack}

    protected State aiState;
    protected bool inAction;

    private Vector2 direction;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(!inAction)
        {
            DecideNewAction();
            inAction = true;
            StartCorrectAction();
        }
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


            MoveTowards(player.transform);
            yield return null;       
        }
    }

    IEnumerator Idle(float idleTime)
    {
        yield return new WaitForSeconds(idleTime);
        inAction = false;
    }

    protected abstract IEnumerator Attack();

    protected virtual void MoveTowards(Transform towards)
    {
        Vector2 toTarget = towards.position - transform.position;

        if(toTarget.x > 0.0f)
        {
            direction.x = 1.0f;
        }
        else
        {
            direction.x = -1.0f;
        }

        //transform.LookAt(towards);  //Might need to change worldUp here???
        transform.position = Vector2.MoveTowards(transform.position, towards.position, Time.deltaTime * enemySpeed);
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
}
