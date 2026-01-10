using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Money : MonoBehaviour
{
    public float getMoney;

    public GameObject nowMoney;
    public GameObject getMoneyObject; // Textオブジェクト


    public int possessionCoin_num = 0; // スコア変数
    // Start is called before the first frame update
    void Start()
    {
        possessionCoin_num = PlayerPrefs.GetInt("possessionCoin", possessionCoin_num);

    }

    // Update is called once per frame
    void Update()
    {
        Text money_Text = nowMoney.GetComponent<Text>();
        // テキストの表示を入れ替える
        money_Text.text = getMoney.ToString("f1");

    }

    public void AddMoney(float moneyAmount)
    {
        getMoney += moneyAmount;
        // オブジェクトからTextコンポーネントを取得
        Text getMoney_Text = getMoneyObject.GetComponent<Text>();

        // テキストの表示を入れ替える
        getMoney_Text.text = "+" + moneyAmount.ToString("f1") + "<size=128>sec</size>";
    }

    // 現在のコインを返す（読み取り専用）
    public float CurrentMoney
    {
        get { return getMoney; }
    }
}
