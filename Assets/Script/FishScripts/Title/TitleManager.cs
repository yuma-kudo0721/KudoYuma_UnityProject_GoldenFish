using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button startButton;

    private string playerName;
    public GameObject possessionCoinText;

    private TMP_Text buttonText; // ボタンのテキスト表示用

    const string SAVED_NAME_KEY = "SAVED_PLAYER_NAME";

    void Start()
    {
        PlayFabSettings.TitleId = "119669";

        // startButton の子TextMeshProUGUIを取得
        buttonText = startButton.GetComponentInChildren<TMP_Text>();

        startButton.onClick.AddListener(OnStartClicked);

        // 初期メッセージを表示
        SetButtonText("名前を入力してスタート");

        // 保存されている名前があればInputFieldにセットする
        if (PlayerPrefs.HasKey(SAVED_NAME_KEY))
        {
            nameInputField.text = PlayerPrefs.GetString(SAVED_NAME_KEY);
        }
        AutoLogin();


        var coin = PlayerPrefs.GetFloat("possessionCoin", 0f);
        possessionCoinText.GetComponent<TextMeshProUGUI>().text = coin.ToString("F1");


    }

    void AutoLogin()
    {
        string savedName = PlayerPrefs.GetString(SAVED_NAME_KEY, "");
        if (string.IsNullOrEmpty(savedName))
        {
            // 名前が保存されていなければ自動ログインは行わない
            return;
        }

        playerName = savedName;
        SetButtonText("自動ログイン中...");

        var request = new LoginWithCustomIDRequest
        {
            CustomId = GetOrCreateCustomID(),
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request,
            result =>
            {
                SetButtonText("自動ログイン成功");

            },
            error =>
            {
                SetButtonText("自動ログイン失敗: " + error.GenerateErrorReport());
            });
    }

    void OnStartClicked()
    {
        playerName = nameInputField.text;

        if (playerName.Length < 3 || playerName.Length > 25)
        {
            SetButtonText("名前は3文字以上、25文字以下で入力してください");
            return;
        }

        if (string.IsNullOrWhiteSpace(playerName))
        {
            SetButtonText("名前が空です！");
            return;
        }

        // 名前を保存する
        PlayerPrefs.SetString(SAVED_NAME_KEY, playerName);
        PlayerPrefs.Save();
        SetButtonText("ログイン中...");
        LoginToPlayFab();
    }

    void LoginToPlayFab()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = GetOrCreateCustomID(),
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    string GetOrCreateCustomID()
    {
        const string KEY = "PLAYFAB_CUSTOM_ID";

        if (PlayerPrefs.HasKey(KEY))
        {
            return PlayerPrefs.GetString(KEY);
        }
        else
        {
            string newId = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString(KEY, newId);
            return newId;
        }
    }

    void OnLoginSuccess(LoginResult result)
    {
        SetButtonText("ログイン成功！名前設定中...");
        SetPlayerName();
    }

    void OnLoginFailure(PlayFabError error)
    {
        SetButtonText("ログイン失敗: " + error.GenerateErrorReport());
    }

    void SetPlayerName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = playerName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request,
            result =>
            {
                SetButtonText("名前設定成功: " + playerName);
                // 1秒後くらいにゲームシーンへ遷移するならコルーチンで遅延も可
                SceneManager.LoadScene("Web");
            },
            error =>
            {
                SetButtonText("名前設定失敗: " + error.GenerateErrorReport());
            });
    }

    // テキスト更新用ヘルパー
    void SetButtonText(string message)
    {
        if (buttonText != null)
        {
            buttonText.text = message;
        }
        else
        {
            Debug.LogWarning("buttonTextが設定されていません");
        }
    }
}
