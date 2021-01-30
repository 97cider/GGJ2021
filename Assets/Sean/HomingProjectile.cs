using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    [SerializeField] private float _homingRange;
    [SerializeField] private string intersectLayerName;
    private GameObject _homingCandidate;

    private int layer;
    void Awake() 
    {
        layer = LayerMask.GetMask(intersectLayerName);
    }
    protected override void Update()
    {
        if (_homingCandidate == null)
        {
            Vector2 target = direction * speed * Time.deltaTime;
            this.transform.position += new Vector3(target.x, target.y, 0.0f);

            RaycastHit2D[] hits = Physics2D.CircleCastAll(this.transform.position, _homingRange, new Vector2(0.0f, 0.0f), 0.0f, layer); 
            if (hits.Length > 0) 
            {
                this._homingCandidate = hits[0].collider.gameObject;
            }
        }
        else 
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, this._homingCandidate.transform.position, speed * Time.deltaTime);
        }
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
