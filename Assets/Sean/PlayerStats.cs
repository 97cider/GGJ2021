using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{

    public bool canMove;
    [SerializeField] private int _currentHealth;
    private int _maxHealth = 1000;

    [SerializeField] private float _movementSpeed;

    [SerializeField] private float _jumpModifier;

    [SerializeField] private Weapon _currentWeapon;

    private int _completedLevels;
    public UnityEvent playerdieEvent;
    private int _completedRuns;

    [SerializeField] private Accessory _currentAccessory;

    [SerializeField] private Vector2 _orientation;

    [SerializeField] private SpriteRenderer _renderer;

    [SerializeField] private SpriteRenderer _weaponRenderer;
    private PlayerEffectsController _effects;


    public int getMaxHP(){
        return this._maxHealth;
    }
    public int getCurrentHp(){
        return this._currentHealth;
    }
    void Awake()
    {
        _effects = this.GetComponent<PlayerEffectsController>();
        if(playerdieEvent == null){
            playerdieEvent = new UnityEvent();
        }
        Debug.LogWarning("INIT!");
        if (_currentAccessory != null)
        {
            this._maxHealth = _currentAccessory.maxHPModifier;
            Debug.Log($"Max health via null chek: {this._maxHealth}");
            this._currentHealth = this._maxHealth;
            // Modify properties based on current accessory
            var w = GetWeapon();
            this.EquipWeapon(w);
        }
    }
    public void Start()
    {
  
    }

    public Weapon GetWeapon()
    {
        return this._currentWeapon;
    }

    public void EquipWeapon(Weapon w)
    {
        if (this._currentAccessory)
        {
            w.modifyWeaponStats(this._currentAccessory);
        }

        _weaponRenderer.sprite = w.weaponSprite;

        this._currentWeapon = w;
    }
    public void changeMove(){
        this.canMove = !this.canMove;
    }
    public Vector2 GetOrientation()
    {
        return this._orientation;
    }
    private void Update() {
        //Debug.Log($"Current Max Hp: {this._maxHealth}");
    }    
    public void TakeDamage( int damage)
    {
        this._currentHealth -= damage;
        StartCoroutine(DamageFlicker());
        _effects.ShakeCameraOnHit();
        _effects.updateGui();
        if (this._currentHealth <= 0.0f)
        {
            this.Die();
        }
    }

    public Accessory getCurrentAccessory()
    {
        return _currentAccessory;
    }
    public void setCurrentAccessory( Accessory a)
    {
        _currentAccessory = a;
    }
    public void Die()
    {
        StopAllCoroutines();
        playerdieEvent.Invoke();
        Destroy(this.gameObject);
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
