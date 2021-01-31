using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] protected float spreadAngle;

    public override void Attack(Vector3 origin, Vector2 orientation)
    {
        //base.Attack(origin, orientation);
        
    }
}
