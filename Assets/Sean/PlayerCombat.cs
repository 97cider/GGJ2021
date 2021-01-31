using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] private Transform _projectileOrigin;

    private PlayerStats _player;

    private Weapon _currentWeapon => _player.GetWeapon();

    public float weaponCooldown;

    private float maxCooldown => _player.GetWeapon().GetWeaponCooldown();

    [SerializeField] private Animator Animator;

    [SerializeField] private string AttackAnimName;

    private PlayerEffectsController _effects;

    

    void Awake () 
    {
        _player = this.GetComponent<PlayerStats>();
        _effects = this.GetComponent<PlayerEffectsController>();
        weaponCooldown = maxCooldown;
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return Animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    void EquipWeapon() 
    {
        weaponCooldown = maxCooldown;
    }
    void Update() 
    {
        if (maxCooldown > 0.0f)
        {
            weaponCooldown -= Time.deltaTime;
        }
        // handle combat
        if (Input.GetMouseButtonDown(0) && !GameManager.isPaused) 
        {
            if (_currentWeapon != null) 
            {
                if (weaponCooldown <= 0.0f)
                {
                    Animator.Play(AttackAnimName);
                    _effects.ShakeCameraOnAttack();
                    weaponCooldown = maxCooldown;
                    print("Hey we attacked with " + this._currentWeapon.GetWeaponName());
                    print($"Hey we should be doing {this._currentWeapon._weaponDamage}");
                    Vector2 orientation = new Vector2(this._projectileOrigin.localPosition.x / Mathf.Abs(this._projectileOrigin.localPosition.x), 0.0f);
                    _currentWeapon.Attack(this._projectileOrigin.position, orientation);
                }
            }
        }
    }
}
