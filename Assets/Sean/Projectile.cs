using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector2 direction;

    public float damage;

    // public bool 

    public float rotation;

    void OnCollisionEnter() 
    {
        GameObject.Destroy(this);
    }

    void OnBecameInvisible()
    {
        // GameObject.Destroy(this);
        Destroy(this.gameObject, 1.0f);
        // this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemyCandidate = other.GetComponent<Enemy>();
        if (enemyCandidate != null)
        {
            enemyCandidate.TakeDamage(this.damage);
        }
        Destroy(this.gameObject);
    }

    void Update()
    {
        Vector2 target = direction * speed * Time.deltaTime;
        this.transform.position += new Vector3(target.x, target.y, 0.0f);
    }
}
