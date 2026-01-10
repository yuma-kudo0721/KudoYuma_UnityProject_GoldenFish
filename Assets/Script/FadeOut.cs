using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeOut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fadeOut());


    }

    IEnumerator fadeOut()
    {


        yield return new WaitForSeconds(3);
        // アルファ値を３秒かけて０にする
        GetComponent<Renderer>().material.DOFade(0, 6);

    }
}
