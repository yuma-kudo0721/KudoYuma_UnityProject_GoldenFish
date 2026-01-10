using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class RunAway : FishBase
{

    public float detectionRange = 2.0f;

    public float escapeSpeed = 2.0f;
    GameObject poiPosi;

    public override void Update()
    {
        base.Update();
        poiPosi = GameObject.FindWithTag("Poi");
        if (poiPosi != null)
        {
            float distance = Vector2.Distance(transform.position, poiPosi.transform.position);
            if (distance <= detectionRange)
            {
                Vector2 escapeDirection = (transform.position - poiPosi.transform.position).normalized;
                rb.velocity = escapeSpeed * escapeDirection;

            }
        }

    }
}
