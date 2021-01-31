using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 direction;

    public int damage;

    public bool hasDuration; 

    public float duration;

    public float rotation;

    public bool pierceWalls;

    public bool pierceTargets;

    void OnCollisionEnter() 
    {
        GameObject.Destroy(this);
    }

    void OnBecameInvisible()
    {
        // GameObject.Destroy(this);
        this.Dispose();
        // this.gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemyCandidate = other.GetComponent<Enemy>();
        if (enemyCandidate != null)
        {
            enemyCandidate.TakeDamage(this.damage);
            if (!pierceTargets)
            {
                this.Dispose();
            }
            return;
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Default") && !pierceWalls)
        {
            this.Dispose();
            return;
        }
    }

    public virtual void OnShoot()
    {
        if(direction.x != 0.0f)
        {
            Vector3 projScale = this.transform.localScale;
            projScale.x *= (direction.x / Mathf.Abs(direction.x));
            this.transform.localScale = projScale;
        }
    }

    protected void Dispose() 
    {
        Destroy(this.gameObject);
    }

    protected virtual void Update()
    {
        Vector2 target = direction * speed * Time.deltaTime;
        this.transform.position += new Vector3(target.x, target.y, 0.0f);

        if (this.hasDuration) 
        {
            this.duration -= Time.deltaTime;
            if (this.duration <= 0.0f)
            {
                this.Dispose();
            }
        }
    }
}
