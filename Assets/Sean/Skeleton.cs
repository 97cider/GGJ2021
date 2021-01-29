using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public Color flickerColor;

    private SpriteRenderer _renderer;

    protected override void Awake()
    {
        base.Awake();
        this._renderer = this.GetComponent<SpriteRenderer>();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        // flash colors
        if(base._enemyHealth > 0)
        {
            iTween.ShakeRotation(this.gameObject, new Vector3(0.0f, 0.0f, 20.0f), 0.3f);
            StartCoroutine(DamageFlicker());
        }
    }

    public override void Die()
    {
        StopAllCoroutines();
        base.Die();
    }

    private IEnumerator DamageFlicker()
    {
        for(int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.05f);
            _renderer.material.SetColor("_AdditiveTint", flickerColor);
            yield return new WaitForSeconds(0.05f);
            _renderer.material.SetColor("_AdditiveTint", Color.black);
        }
    }
}
