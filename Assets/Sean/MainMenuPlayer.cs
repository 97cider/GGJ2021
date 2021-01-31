using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlayer : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;

    public int moveDir;

    public float moveSpeed;

    public float holdTime;

    public bool canMove;

    public List<Sprite> weaponSprites;

    public SpriteRenderer weaponRenderer;

    public float chanceForSkelly = 0.2f;

    public GameObject skelly;

    private float currentHoldTime;
    // Start is called before the first frame update
    void Start()
    {
        currentHoldTime = 1.0f;
        holdTime = Random.Range(3.0f, 10.0f);
        canMove = false;
    }

    private void FlipSprite()
    {
        Vector3 scale = this.transform.localScale;
        scale.x *= -1;
        this.transform.localScale = scale;

        // update weapon sprites
        int index = Random.Range(0, weaponSprites.Count);
        weaponRenderer.sprite = weaponSprites[index];

        float skellySpawn = Random.Range(0.0f, 1.0f);
        if (skellySpawn <= chanceForSkelly)
        {
            skelly.SetActive(true);
        }
        else
        {
            skelly.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) 
        {
            if (moveDir < 0)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, leftPoint.position, moveSpeed * Time.deltaTime);
                if(this.transform.position.x <= leftPoint.position.x)
                {
                    moveDir *= -1;
                    holdTime = Random.Range(3.0f, 10.0f);
                    canMove = false;
                    FlipSprite();
                }
            }
            else 
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position, rightPoint.position, moveSpeed * Time.deltaTime);
                if (this.transform.position.x >= rightPoint.position.x)
                {
                    moveDir *= -1;
                    holdTime = Random.Range(3.0f, 10.0f);
                    canMove = false;
                    FlipSprite();
                }
            }
        }
        else 
        {
            currentHoldTime -= Time.deltaTime;
            if (currentHoldTime <= 0.0f)
            {
                currentHoldTime = holdTime;
                canMove = true;
            }
        }
    }
}
