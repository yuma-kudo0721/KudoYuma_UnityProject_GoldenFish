using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingButton : MonoBehaviour
{


    public void RankingButtonClick()
    {
        if (Ranking.Instance != null)
        {
            Ranking.Instance.GetRanking();
        }

    }
}
