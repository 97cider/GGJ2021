using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{

    public bool canMove;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    [SerializeField] private float _movementSpeed;

    [SerializeField] private float _jumpModifier;

    [SerializeField] private Weapon _currentWeapon;

    [SerializeField] private Item _currentItem;

    [SerializeField] private Vector2 _orientation;

    public UnityEvent PlayerDeathEvent;

    public Weapon GetWeapon()
    {
        return this._currentWeapon;
    }
    public void changeMove(){
        this.canMove = !this.canMove;
    }
    public Vector2 GetOrientation()
    {
        return this._orientation;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        
        if(_currentHealth <= 0.0f)
        {
            PlayerDeathEvent.Invoke();
        }
    }
}
