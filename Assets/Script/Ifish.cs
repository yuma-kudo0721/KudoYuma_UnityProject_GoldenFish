using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IFish
{
    int GetScore(); // スコアを返す
    float GetTime(); // スコアを返す
    float GetMoney(); // スコアを返す
    void OnDefeated(); // 倒されたときの挙動

}
