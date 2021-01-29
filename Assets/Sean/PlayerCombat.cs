using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] private Transform _projectileOrigin;

    private PlayerStats _player;

    private Weapon _currentWeapon => _player.GetWeapon();

    void Awake () 
    {
        _player = this.GetComponent<PlayerStats>();
    }
    void Update() 
    {
        // handle combat
        if (Input.GetMouseButtonDown(0) && !GameManager.isPaused) 
        {
            if (_currentWeapon != null) 
            {
                print("Hey we attacked with " + this._currentWeapon.GetWeaponName());
                _currentWeapon.Attack(this._projectileOrigin.position, _player.GetOrientation());
            }
        }
    }
}
