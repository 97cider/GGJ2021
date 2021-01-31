using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAI : Enemy
{
    public Color flickerColor;

    public float moveSpeed;

    private SpriteRenderer _renderer;

    private int moveDir = 1;

    public GameObject skeletonDeathPrefab;

    private GameObject _playerTarget;

    [Header("Attacks")]
    public float playerDetectRange;

    public float attackCooldown;
    private float _currentAttackCooldown;

    public float _shotForce;

    public GameObject enemyProjectile;

    public Transform enemyShootPoint;

    public Vector2 projectileOrientation;

    public float projDamage;
    
    public bool isMoving = true;

    private bool canMove;

    private Animator _anim;

    [Header("Animations")]
    public string idleAnimationName;
    public string jumpAnimationName;

    private int layer;

    private Rigidbody2D _rigid;

    public Vector2 jumpDir;

    public float jumpForce;

    public Transform groundCheck;

    private bool m_Grounded;

    public UnityEvent landEvent;

    [SerializeField] private LayerMask m_WhatIsGround;

    public PlayerStats _mc;

    protected override void Awake()
    {
        base.Awake();
        _mc = FindObjectOfType<PlayerStats>();
        this._enemyMaxHealth = this._enemyMaxHealth * (1 + ((.25f) * _mc.runsCompleted));
        Debug.LogError($"BOSS MAX HP: {this._enemyMaxHealth}");
        this._renderer = this.GetComponent<SpriteRenderer>();
        this._anim = this.GetComponent<Animator>();
        this._rigid = this.GetComponent<Rigidbody2D>();
        layer = LayerMask.GetMask("Player");
        _currentAttackCooldown = attackCooldown;
        canMove = true;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        // flash colors
        if(base._enemyHealth > 0)
        {
            StartCoroutine(DamageFlicker());
        }
    }

    public void StopMoving()
    {
        this.isMoving = false;
    }

    public void StartMoving()
    {
        this.isMoving = true;
    }

    public void PlayLandAnimation()
    {
        this._anim.Play(idleAnimationName);
        if(_mc != null)
        {
            _mc.PlayShockWave();
        }
    }

    public void FlipCharacter()
    {
        Vector3 skellyScale = this.transform.localScale;
        skellyScale.x *= -1;
        this.transform.localScale = skellyScale;
        moveDir *= -1;
    }

    void Attack()
    {
        Debug.Log("Boss is jumping!");
        _anim.Play(jumpAnimationName);
        Vector2 force = new Vector2(jumpDir.x * moveDir, jumpDir.y);
        _rigid.AddForce(jumpDir * jumpForce * force, ForceMode2D.Impulse);
        _currentAttackCooldown = attackCooldown;
        
    }

    public void ShootProjectiles()
    {
        Debug.Log("projksss");
        GameObject lfB = GameObject.Instantiate(enemyProjectile, enemyShootPoint.position, Quaternion.identity);
        Projectile leftProjectile = lfB.GetComponent<Projectile>();
        leftProjectile.direction = new Vector2(1.0f, 0.0f);
        leftProjectile.damage = 1;
        leftProjectile.duration = 0.5f;
        leftProjectile.speed = 10.0f;

        GameObject rfB = GameObject.Instantiate(enemyProjectile, enemyShootPoint.position, Quaternion.identity);
        Projectile rightProjectile = rfB.GetComponent<Projectile>();
        rightProjectile.direction = new Vector2(-1.0f, 0.0f);
        rightProjectile.damage = 1;
        rightProjectile.duration = 0.5f;
        rightProjectile.speed = 10.0f;
    }

    public void ResetVelocity()
    {
        this._rigid.velocity = Vector2.zero;
    }

    void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.1f, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                

                if (!wasGrounded)
                {
                    landEvent.Invoke();
                }
            }
        }
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }

        _currentAttackCooldown -= Time.deltaTime;
        if (_currentAttackCooldown <= 0.0f) 
        {
                this.Attack();
        }
    }

    public override void Die()
    {
        StopAllCoroutines();
        base.Die();
        Destroy(this.gameObject);
    }

    private IEnumerator DamageFlicker()
    {
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.05f);
            _renderer.material.SetColor("_AdditiveTint", flickerColor);
            yield return new WaitForSeconds(0.05f);
            _renderer.material.SetColor("_AdditiveTint", Color.clear);
        }
    }
    public void DestroyOnDeath()
    {
        Destroy(this.gameObject);
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

    public void CanMove(bool moveThatGearUp)
    {
        canMove = moveThatGearUp;
    }
}