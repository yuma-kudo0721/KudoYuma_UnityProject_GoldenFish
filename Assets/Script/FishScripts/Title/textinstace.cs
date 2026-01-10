using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textinstace : MonoBehaviour
{
    void Start()
    {
        if (Ranking.Instance != null)
        {
            Ranking.Instance.AttachText(GetComponent<TMP_Text>());
        }
    }

}
