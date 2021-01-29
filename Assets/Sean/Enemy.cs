using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Enemy class dictates anything that is interactable with projectiles
// chests, barrells, destroyable traps should all count as enemies for the time being
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _enemyHealth;

    [SerializeField] private float _enemyMaxHealth;

    public UnityEvent DieEvents;

    public void Awake() 
    {
        this._enemyHealth = this._enemyMaxHealth;
    }

    public void TakeDamage(float damage)
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
