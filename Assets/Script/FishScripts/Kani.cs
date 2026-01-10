using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kani : FishBase
{
    protected override void MoveTowardsTarget()
    {
        transform.position =
        Vector3.MoveTowards(transform.position, movePosition, speed * Time.deltaTime);

        Vector3 dir = movePosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
