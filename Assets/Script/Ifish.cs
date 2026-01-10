using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IFish
{
    int GetScore();      // この魚を倒したときに加算されるスコアを返す
    float GetTime();     // この魚を倒したときに加算される制限時間を返す
    float GetMoney();    // この魚を倒したときに得られる金を返す
    void OnDefeated();   // 倒されたときの挙動
}
