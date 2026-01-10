using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public GameObject score_object; // Textオブジェクト
    public GameObject scoreMax_object; // Textオブジェクト
    public int score_num = 0; // スコア変数
    public int scoreMax_num = 0; // スコア変数


    // Start is called before the first frame update
    void Start()
    {
        // スコアのロード
        score_num = PlayerPrefs.GetInt("SCORE", 0);
        scoreMax_num = PlayerPrefs.GetInt("ScoreMax", scoreMax_num);

    }

    // Update is called once per frame
    void Update()
    {



        // オブジェクトからTextコンポーネントを取得
        Text score_text = score_object.GetComponent<Text>();
        Text scoreMax_text = scoreMax_object.GetComponent<Text>();
        // テキストの表示を入れ替える
        score_text.text = "Score :" + score_num;
        scoreMax_text.text = "MaxScore :" + scoreMax_num;

        if (score_num >= scoreMax_num)
        {
            PlayerPrefs.SetInt("ScoreMax", score_num);
            scoreMax_num = score_num;


        }



    }

    public void AddScore(int amount)
    {
        score_num += amount;
    }


}
