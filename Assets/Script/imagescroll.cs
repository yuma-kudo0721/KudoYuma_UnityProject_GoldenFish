using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imagescroll : MonoBehaviour
{
    private const float MAX_OFFSET = 1f;
    private const string PROPERTY_NAME = "_MainTex";

    [SerializeField] private Vector2 _offsetSpeed;
    [SerializeField] private Material _material;

    private void Reset()
    {
        // コンポーネントがアタッチされたタイミングでマテリアルを取得する
        if (TryGetComponent(out Image image))
        {
            _material = image.material;
        }
    }

    private void Update()
    {
        if (_material != null)
        {
            // xだけ負方向に増やして、Mathf.Repeatでうまくループさせる
            float xOffset = Time.time * _offsetSpeed.x;
            float x = Mathf.Repeat(xOffset, 1f); // ← 左→右に動く
            var offset = new Vector2(x, 0f); // yを固定
            _material.SetTextureOffset(PROPERTY_NAME, offset);
        }
    }

    private void OnDestroy()
    {
        // オブジェクトが破棄されるタイミングに位置をリセットする
        if (_material != null)
        {
            _material.SetTextureOffset(PROPERTY_NAME, Vector2.zero);
        }
    }
}
