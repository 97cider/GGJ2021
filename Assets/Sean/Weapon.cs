using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private string _weaponName;

    [SerializeField] private string _weaponDescription;

    [SerializeField] private float _weaponDamage;

    [SerializeField] private bool isProjectileWeapon;

    [SerializeField] private GameObject _projectilePrefab;

    [SerializeField] private float _projectileSpeed;

    [SerializeField] private float _projectileDuration;

    [SerializeField] private float _fireRate;

    [SerializeField] private bool _forceOrientation;
    [SerializeField] private Vector3 projectileScale;

    [SerializeField] private Vector2 _overrideOrientation;
    private PlayerStats _player;

    public void EquipWeapon(PlayerStats player) 
    {
        _player = player;
    }

    public string GetWeaponName()
    {
        return this._weaponName;
    }

    public void modifyWeaponStats(Accessory a){
        // Transfer over properties, multiplicitavely.
        _weaponDamage =_weaponDamage * a.baselineDamageModifier;
    }
    public float GetWeaponCooldown()
    {
        return this._fireRate;
    }

    public virtual void Attack(Vector3 origin, Vector2 orientation) 
    {
        if (isProjectileWeapon) 
        {
            GameObject boolet = GameObject.Instantiate(_projectilePrefab, origin, Quaternion.identity);
            boolet.transform.localScale = projectileScale;
            Projectile proj = boolet.GetComponent<Projectile>();
            proj.speed = _projectileSpeed;
            if (!_forceOrientation) 
            {
                proj.direction = orientation;
            }
            else
            {
                float xDir = orientation.x * _overrideOrientation.x;
                float yDir = _overrideOrientation.y;
                proj.direction = new Vector2(xDir, yDir);
            }
            proj.damage = _weaponDamage;
            if (_projectileDuration > 0.0f)
            {
                proj.hasDuration = true;
                proj.duration = _projectileDuration;
            }
            proj.OnShoot();
        }
    }
}
