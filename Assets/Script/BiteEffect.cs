using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteEffect : MonoBehaviour
{
    [SerializeField] float destoroyCount;

    void Start()
    {
        StartCoroutine(ScaleUpCoroutine());

    }


    IEnumerator ScaleUpCoroutine()
    {
        gameObject.layer = LayerMask.NameToLayer("Escape");
        yield return new WaitForSeconds(destoroyCount);
        Destroy(gameObject);
    }
}
