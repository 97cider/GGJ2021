using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{ 
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        PlayerStats playerCandidate = other.GetComponent<PlayerStats>();

        if (playerCandidate != null)
        {
            playerCandidate.TakeDamage(this.damage);
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


}

