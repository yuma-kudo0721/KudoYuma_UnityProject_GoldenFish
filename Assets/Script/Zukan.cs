using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zukan : MonoBehaviour
{
    public RectTransform contentRectTransform;
    public GameObject buttonPrefab;     // 画像を表示するButtonプレハブ
    public Sprite[] sprites;            // 表示したい画像
    
    

    private void Start()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            var buttonObj = Instantiate(buttonPrefab, contentRectTransform);

            // 子のImageを探してSpriteを差し替え
            var imageObj = buttonObj.transform.Find("Image");
            if (imageObj != null)
            {
                Image img = imageObj.GetComponent<Image>();
                if (img != null)
                {
                    img.sprite = sprites[i];
                    RectTransform rt = img.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(200, 200);//高さを統一する
                    img.preserveAspect = true;             // アスペクト比保持（崩れないように）
                }

            }

        }
    }
}
