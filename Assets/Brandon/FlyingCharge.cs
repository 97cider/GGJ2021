using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCharge : FlyingAI
{
    [SerializeField] protected float overshootDistance;
    [SerializeField] protected float chargeSpeed;

    protected override IEnumerator Attack()
    {
        Vector2 x = player.transform.position - gameObject.transform.position;
        Vector2 normalizedX = x.normalized;
        Vector2 overshoot = normalizedX * overshootDistance;

        Vector2 endPoint = (Vector2)player.transform.position + overshoot;
        Vector2 startPoint = gameObject.transform.position;

        float timeSpentCharging = ((endPoint - startPoint).magnitude) / chargeSpeed;

        float t = 0.0f;

        while(t < 1)
        {
            t += Time.deltaTime / timeSpentCharging;
            transform.position = Vector2.Lerp(startPoint, endPoint, t);
            yield return null;
        }

        inAction = false;
    }
}
