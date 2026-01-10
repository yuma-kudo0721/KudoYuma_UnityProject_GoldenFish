using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kuzira : FishBase
{
    [SerializeField] GameObject splashParticle;
    [SerializeField] float splashTime;
    [SerializeField] float startSplashTime;
    [SerializeField] GameObject splashPosition;

    public override void Update()
    {
        base.Update();
        splashTime -= Time.deltaTime;

        if (splashTime <= 0f)
        {
            Instantiate(splashParticle, splashPosition.transform.position, Quaternion.identity);
            splashTime = startSplashTime;

        }

    }
    protected override Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0f);
    }


}
