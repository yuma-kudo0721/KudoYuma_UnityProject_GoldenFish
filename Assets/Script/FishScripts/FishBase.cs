using UnityEngine;

public abstract class FishBase : MonoBehaviour, IFish
{
    [Header("移動速度")]
    public float speed = 8f;

    protected Vector3 movePosition;
    protected Vector3 deletePosition;

    protected Rigidbody2D rb;
    [Header("キャラの滞在時間")]
    public float countDown = 5.0f;

    public int score = 100;
    [Header("増加時間")]
    public float time = 1f;
    public float acquisitionMoney = 1f;
    public GameObject defeatEffect;

    public Sprite[] moveSprites;

    public float MoveAniTime = 0.1f;
    public float MoveAniTimer = 0f;
    int MoveAniSpriteNum = 0;
    public SpriteRenderer spriteRenderer;

    public virtual void Start()
    {
        movePosition = GetRandomPosition();
        deletePosition = GetRandomDeletePosition();

    }

    public virtual int GetScore()
    {
        return score;
    }

    public virtual float GetTime()
    {
        return time;
    }

    public virtual float GetMoney()
    {
        return acquisitionMoney;
    }

    public virtual void Move()//移動アニメーションを管理
    {
        if (spriteRenderer != null)
        {
            if (MoveAniTimer >= MoveAniTime)
            {
                MoveAniSpriteNum = (MoveAniSpriteNum + 1) % moveSprites.Length;
                spriteRenderer.sprite = moveSprites[MoveAniSpriteNum];
                MoveAniTimer = 0;
                spriteRenderer.color = Color.white;
                ;
            }
            else
            {
                MoveAniTimer += Time.deltaTime;
            }
        }
    }

    //魚がすくわれたときの演出
    public virtual void OnDefeated()
    {
        ScaleEffect se = GetComponent<ScaleEffect>();

        if (se != null)
        {
            se.enabled = true;
        }

        if (defeatEffect != null)
        {
            Instantiate(defeatEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public virtual void Update()
    {
        countDown -= Time.deltaTime;

        if (Vector3.Distance(transform.position, movePosition) < 0.01f)
        {
            if (countDown <= 0)
            {
                // 時間が０になったらdeletePositionに移動
                movePosition = deletePosition;
            }
            else
            {
                movePosition = GetRandomPosition();
            }
        }

        // deletePositionに到達したら魚消す
        if (countDown <= 0 && Vector3.Distance(transform.position, deletePosition) < 0.01f)
        {
            Destroy(gameObject);
        }

        MoveTowardsTarget();
        Move();
    }



    protected virtual void MoveTowardsTarget()
    {
        transform.position =
        Vector3.MoveTowards(transform.position, movePosition, speed * Time.deltaTime);

        Vector3 dir = movePosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }


    protected virtual Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-9f, 9f), Random.Range(-4f, 4f), 0f);
    }

    protected virtual Vector3 GetRandomDeletePosition()
    {
        return new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), 0);


    }
}
