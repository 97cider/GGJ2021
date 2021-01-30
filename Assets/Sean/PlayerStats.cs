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

    [SerializeField] private SpriteRenderer _renderer;
    private PlayerEffectsController _effects;

    void Awake()
    {
        _effects = this.GetComponent<PlayerEffectsController>();
    }

    public Weapon GetWeapon()
    {
        return this._currentWeapon;
    }

    public Vector2 GetOrientation()
    {
        return this._orientation;
    }

    public void TakeDamage(float damage)
    {
        this._currentHealth -= damage;
        StartCoroutine(DamageFlicker());
        _effects.ShakeCameraOnHit();
        if (this._currentHealth <= 0.0f)
        {
            this.Die();
        }
    }

    public void Die()
    {
        StopAllCoroutines();
    }

    private IEnumerator DamageFlicker()
    {
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.05f);
            _renderer.material.SetColor("_AdditiveTint", Color.white);
            yield return new WaitForSeconds(0.05f);
            _renderer.material.SetColor("_AdditiveTint", Color.clear);
        }
    }
}
