using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datu : FishBase
{





   
    protected override Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0f);
    }
}
