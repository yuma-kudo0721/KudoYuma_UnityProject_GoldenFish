using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

// <summary>
// PlayFabでオンラインランキング処理を行うクラス
// </summary>
public class Ranking : MonoBehaviour
{
    public static Ranking Instance { get; private set; }
    [SerializeField]
    private TMP_Text rankingText; // ランキング表示用のTextMeshProUGUI
    public bool hasSubmittedScore = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // これでこのオブジェクトはシーン切り替えで消えなくなる
        }
        else
        {
            Destroy(gameObject); // すでにInstanceがあれば重複破棄
        }
    }

    public void AttachText(TMP_Text text)
    {
        rankingText = text;
        Debug.Log("rankingText アタッチ成功（外部から）");
    }

    // ==============================================================
    // テストプログラム
    // ==============================================================

    // テスト用の仮プログラム
    // ランキングの送受信を行う


    // ==============================================================
    // ランキング更新メソッド
    // ==============================================================

    // プレイヤー名を設定するメソッド
    public void SetUserName(string name)
    {
        // ユーザー名を設定するリクエストを作る
        var request = new UpdateUserTitleDisplayNameRequest
        {
            // ユーザー名の設定
            DisplayName = name
        };

        // リクエストをPlayFabに送信する
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSetUserNameSuccess, OnSetUserNameFailure);

        // 送信成功時の処理
        void OnSetUserNameSuccess(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log("プレイヤー名の変更に成功しました");
        }

        // 送信失敗時の処理
        void OnSetUserNameFailure(PlayFabError error)
        {
            Debug.Log("プレイヤー名の変更に失敗しました");
        }
    }

    // 昇順ランキングのスコア送信メソッド
    public void SubmitScore(int score)
    {
        // 昇順ランキング用にスコアを修正する
        // 降順ランキングの場合はこの作業は省略する
        // 処理の内容と理由は後述
        if (hasSubmittedScore) return; // 一度送信したら何もしない
        hasSubmittedScore = true;
        int modifiedScore = score;

        // 送信する内容を作る
        var statisticUpdate = new StatisticUpdate
        {
            // 統計情報名の指定（LeaderBoards(Legace)で設定した名前）
            StatisticName = "TestRanking",
            // 送信するスコアの代入
            Value = modifiedScore,
        };

        // 作った内容をリクエストにまとめる
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                statisticUpdate
            }
        };

        // リクエストをPlayFabに送信する        
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSubmitScoreSuccess, OnSubmitScoreFailure);

        // 送信成功時の処理
        void OnSubmitScoreSuccess(UpdatePlayerStatisticsResult result)
        {
            Debug.Log("スコアの送信に成功しました");
        }

        // 送信失敗時の処理
        void OnSubmitScoreFailure(PlayFabError error)
        {
            Debug.Log("スコアの送信に失敗しました");
            hasSubmittedScore = false; // 失敗時はリセット
        }
    }

    // ==============================================================
    // ランキング取得メソッド
    // ==============================================================

    // 昇順ランキングのスコア取得メソッド
    public void GetRanking()
    {
        Debug.Log("GetRanking()が呼ばれました");
        // PlayFabに送信するリクエストを作成する
        var request = new GetLeaderboardRequest
        {
            // 統計情報名の指定（LeaderBoards(Legace)で設定した名前）
            StatisticName = "TestRanking",
            // 何位以降のランキングを取得するか指定する
            // 1位から取得する場合は0を代入
            StartPosition = 0,
            // 何件分のランキングデータを取得するか指定する
            // 最大は100
            MaxResultsCount = 10
        };

        // PlayFabにリクエストを送信する
        PlayFabClientAPI.GetLeaderboard(request, OnGetRankingSuccess, OnGetRankingFailure);

        // 送信成功時の処理
        void OnGetRankingSuccess(GetLeaderboardResult leaderboardResult)
        {
            rankingText.text = "";
            // ランキングを表示する仮コード
            foreach (var item in leaderboardResult.Leaderboard)
            {
                // Positionは順位。0から始まるので+1して表示する
                // intの最大値から取得したスコアを引いて、本来のスコアを出力する
                Debug.Log($"{item.Position + 1}位　プレイヤー名：{item.DisplayName}　スコア：{item.StatValue}");
                rankingText.text += $"{item.Position + 1}位　プレイヤー名：{item.DisplayName}　スコア：{item.StatValue}\n";

            }
        }

        // 送信失敗時の処理
        void OnGetRankingFailure(PlayFabError error)
        {
            Debug.Log("ランキングの取得に失敗しました");
        }
    }

    // ユーザー周辺の昇順ランキングのスコア取得メソッド
    void GetRankingAroundPlayer()
    {
        // PlayFabに送信するリクエストを作成する
        var request = new GetLeaderboardAroundPlayerRequest
        {
            // 統計情報名の指定（LeaderBoards(Legace)で設定した名前）
            StatisticName = "TestRanking",
            // 自分と+-5位をあわせて合計11件を取得
            MaxResultsCount = 11
        };

        // PlayFabにリクエストを送信する
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetRankingAroundPlayerSuccess, OnGetRankingAroundPlayerFailure);

        // 送信成功時の処理
        void OnGetRankingAroundPlayerSuccess(GetLeaderboardAroundPlayerResult leaderboardResult)
        {
            // ランキングを表示する仮コード
            foreach (var item in leaderboardResult.Leaderboard)
            {
                // Positionは順位。0から始まるので+1して表示する
                // intの最大値から取得したスコアを引いて、本来のスコアを出力する
                Debug.Log($"{item.Position + 1}位　プレイヤー名：{item.DisplayName}　スコア：{item.StatValue}");
                rankingText.text += $"{item.Position + 1}位　プレイヤー名：{item.DisplayName}　スコア：{item.StatValue}\n";

            }
        }

        // 送信失敗時の処理
        void OnGetRankingAroundPlayerFailure(PlayFabError error)
        {
            Debug.Log("ランキングの取得に失敗しました");
        }
    }
}
