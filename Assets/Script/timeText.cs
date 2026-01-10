using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeText : MonoBehaviour
{

  public Vector3 targetScale = new Vector3(10f, 10f, 10f); // 目指す大きさ
  public float duration = 1f; // 拡大にかける時間（秒）
                              // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(ScaleUpCoroutine());


  }

  // Update is called once per frame
  void LateUpdate()
  {
    // 回転を完全に固定（Z軸0°など）
    transform.rotation = Quaternion.identity;


  }

  IEnumerator ScaleUpCoroutine()
  {
    Vector3 initialScale = Vector3.one;
    float elapsed = 0f;

    while (elapsed < duration)
    {
      // 0〜1の割合を計算
      float t = elapsed / duration;

      // 線形補間（Lerp）で徐々に大きくする
      transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

      elapsed += Time.deltaTime;
      yield return null;
    }

    // 最終スケールをぴったり設定（誤差防止）
    transform.localScale = targetScale;
    Destroy(gameObject);
  }
}
