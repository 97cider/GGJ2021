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

    
    private int _completedLevels;
    private int _completedRuns;

    [SerializeField] private Accessory _currentAccessory;

    [SerializeField] private Vector2 _orientation;

    public UnityEvent PlayerDeathEvent;


    public void Start(){
        // Modify properties based on current accessory
        this._maxHealth = _currentAccessory.maxJumpScalar;
        var w = GetWeapon();
        w.modifyWeaponStats(this._currentAccessory);

    }

    // Set the weapon damage scaled to the current accessory.
    // i.e. glass cannon accessories, where you do a flat increase in damage
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
    public Accessory getCurrentAccessory(){
        return _currentAccessory;
    }
    public void setCurrentAccessory( Accessory a){
        _currentAccessory = a;
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
