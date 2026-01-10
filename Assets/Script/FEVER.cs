using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FEVER : MonoBehaviour
{
    public RectTransform textRect;       // 動かす文字（UIテキストなど）のRectTransform

    public Transform startPoint;         // スタート位置用の空オブジェクト
    public Transform centerPoint;        // 真ん中で止まる位置用の空オブジェクト
    public Transform endPoint;           // 終了位置用の空オブジェクト

    public float enterDuration = 1f;     // 右から真ん中までの時間
    public float stopDuration = 0.5f;    // 真ん中で止まる時間
    public float exitDuration = 1f;      // 真ん中から左への移動時間
    public Text text;
    public float hueSpeed = 1f;


    void Start()
    {
        PlayAnimation();
    }

    void Update()
    {
        float h = Mathf.Repeat(Time.time * hueSpeed, 1f);
        Color rainbowColor = Color.HSVToRGB(h, 1f, 1f);
        text.color = rainbowColor;
    }

    void PlayAnimation()
    {
        // 最初の位置を設定（スタート地点）
        textRect.position = startPoint.position;

        // DOTweenのシーケンスを使って動きを連結
        Sequence seq = DOTween.Sequence();
        seq.Append(textRect.DOMove(centerPoint.position, enterDuration).SetEase(Ease.InOutSine));
        seq.AppendInterval(stopDuration);
        seq.Append(textRect.DOMove(endPoint.position, exitDuration).SetEase(Ease.InSine))
           .OnComplete(() =>
           {
               Destroy(gameObject); // アニメーション終了後に自分を削除
           });
    }
}
