using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFish : MonoBehaviour
{
    public GameObject fish;  //①動かしたいオブジェクトをインスペクターから入れる。
    GameObject poiPosi;
    public int speed = 1;  //オブジェクトが自動で動くスピード調整
    Vector3 movePosition;  //②オブジェクトの目的地を保存




    void Start()
    {
        movePosition = moveRandomPosition();  //②実行時、オブジェクトの目的地を設定
        poiPosi = GameObject.FindWithTag("Poi");

    }

    void Update()
    {



        if (movePosition == fish.transform.position)  //②playerオブジェクトが目的地に到達すると、
        {
            movePosition = moveRandomPosition();  //②目的地を再設定
        }
        if (poiPosi != null)
        {
            this.fish.transform.position = Vector3.MoveTowards(fish.transform.position, poiPosi.transform.position, speed * Time.deltaTime);  //①②playerオブジェクトが, 目的地に移動, 移動速度
            Vector3 dir = poiPosi.transform.position - fish.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            fish.transform.rotation = Quaternion.Euler(0, 0, angle - 90f); // 2D用
        }

    }

    private Vector3 moveRandomPosition()  // 目的地を生成、xとyのポジションをランダムに値を取得 
    {
        Vector3 randomPosi = new Vector3(Random.Range(-7, 7), Random.Range(-4, 4), 5);
        return randomPosi;
    }
}
