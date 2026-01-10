using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shark : FishBase
{
    GameObject fishPosi;

    [SerializeField] GameObject biteEffect;


    public override void Update()
    {
        base.Update();
        fishPosi = GameObject.FindWithTag("Fish");

    }

    protected override void MoveTowardsTarget()
    {


        if (fishPosi == null)
        {
            base.MoveTowardsTarget();
            return;
        }

        // 移動（追跡）
        transform.position = Vector3.MoveTowards(
            transform.position,
            fishPosi.transform.position,
            speed * Time.deltaTime
        );

        // 回転（fishPosiの方向を向く）
        Vector3 dir = fishPosi.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f); // -90f はスプライトの正面向き調整用
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (fishPosi != null && other.gameObject == fishPosi)
        {
            // 座標だけ保存
            Vector3 bitePos = fishPosi.transform.position;

            Instantiate(biteEffect, bitePos, Quaternion.identity);

            // Fishを破壊
            Destroy(fishPosi);

            fishPosi = null;
        }
    }



}
