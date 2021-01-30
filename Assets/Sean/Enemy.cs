using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Enemy class dictates anything that is interactable with projectiles
// chests, barrells, destroyable traps should all count as enemies for the time being
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float _enemyHealth;

    [SerializeField] protected float _enemyMaxHealth;

    [SerializeField] protected float _enemyContactDamage;

    public UnityEvent DieEvents;

    protected virtual void Awake() 
    {
        this._enemyHealth = this._enemyMaxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        this._enemyHealth -= damage;
        if (this._enemyHealth <= 0)
        {
            this.Die();
        }
    }

    public virtual void Die()
    {
        // Die.
        DieEvents.Invoke();
    }
}
