using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceProjectile : Projectile
{

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    public override void OnShoot()
    {
        base.OnShoot();
        this._rb.AddForce(direction * speed, ForceMode2D.Impulse);
    }

    protected override void Update()
    {
        if (this.hasDuration) 
        {
            this.duration -= Time.deltaTime;
            if (this.duration <= 0.0f)
            {
                this.Dispose();
            }
        }
    }

}
