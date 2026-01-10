using System.Collections;
using Unity.Mathematics;
using UnityEngine;


public class squid : FishBase
{
    private bool isMoving = false;

    public float moveDuration = 0.5f;  // 動く時間
    public float stopDuration = 1.0f;  // 止まる時間

    private Animator anim;

    public GameObject sumi;

    private float sumiTimer;

    public float sumiFireTime;







    public override void Start()
    {
        base.Start();
        StartCoroutine(MovePatternLoop());
        anim = gameObject.GetComponent<Animator>();
    }

    public override void Update()
    {
        sumiTimer += Time.deltaTime;

        if (sumiTimer >= sumiFireTime)
        {
            Instantiate(sumi, transform.position, quaternion.identity);

            sumiTimer = 0f;

        }

        if (isMoving)
        {
            MoveTowardsTarget(); // baseのMoveTowardsTargetを使う
            anim.SetBool("move", true);

        }
        else
        {
            anim.SetBool("move", false);

        }

    }

    private IEnumerator MovePatternLoop()
    {
        while (true)
        {
            isMoving = true;
            yield return new WaitForSeconds(moveDuration);  // 動く時間

            isMoving = false;
            yield return new WaitForSeconds(stopDuration);  // 止まる時間
            // 次のランダム移動先に設定（任意）
            movePosition = GetRandomPosition();


        }
    }
}
