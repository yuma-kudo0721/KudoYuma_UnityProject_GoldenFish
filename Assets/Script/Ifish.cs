using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IFish
{
    int GetScore();      // 魚を捕った時のスコア
    float GetTime();     // この魚を捕ったときに加算される時間
    float GetMoney();    // 魚を捕った時の金
    void OnDefeated();   // 倒されたときの挙動
}
