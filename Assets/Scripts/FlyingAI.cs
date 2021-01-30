using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAI : MonoBehaviour
{
    [SerializeField] private GameObject player;

    enum State { Idle, MoveTowards, Attack}

    private State aiState;
    private bool inAction;

    private Vector2 direction;

    [SerializeField] private float maxDistanceToPlayer;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float idleTime;
    [SerializeField] private float moveTime;

    public GameObject _projectilePrefab;
    public Transform projectileOrigin;

    // Start is called before the first frame update
    void Awake()
    {
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

    IEnumerator Attack()
    {
        DoAttack();
        inAction = false;
        yield break;
    }

    protected virtual void DoAttack()
    {
        GameObject boolet = GameObject.Instantiate(_projectilePrefab, projectileOrigin.position, Quaternion.identity);
        EnemyProjectile proj = boolet.GetComponent<EnemyProjectile>();

        proj.speed = 5.0f;
        proj.damage = 2.0f;
        proj.hasDuration = true;
        proj.duration = 3.0f;
        proj.pierceTargets = true;
        proj.direction = player.transform.position - proj.transform.position;
        proj.direction.Normalize();
        
        //proj.OnShoot(); ...what does this do..?
    }

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

    private bool IsCloseToPlayer()
    {
        bool closeToPlayer = false;

        if (Vector2.Distance(transform.position, player.transform.position) < maxDistanceToPlayer)
        {
            closeToPlayer = true;
        }

        return closeToPlayer;
    }
}
