using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;

    [SerializeField] private float _movementSpeed;

    [SerializeField] private float _jumpModifier;

    [SerializeField] private Weapon _currentWeapon;

    [SerializeField] private Item _currentItem;

    [SerializeField] private Vector2 _orientation;

    public Weapon GetWeapon()
    {
        return this._currentWeapon;
    }

    public Vector2 GetOrientation()
    {
        return this._orientation;
    }
}
