using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;



public class Main : MonoBehaviour
{
    public TimeManager timeManager;
    public Money money;
    public GameObject finishTxt;
    public GameObject poi;
    public GameObject hand;
    public GameObject rainBow;
    public GameObject posi;

    public GameObject feverPrefab;

    public Transform startPoint;
    public Transform centerPoint;
    public Transform endPoint;

    public ScoreManager scoreManager;

    public Slider mySlider;
    [SerializeField] Slider handSlider;
    public Transform canvasTransform;

    public float totalCoin;



    /* [Header("カメラを割り当てる")]
     public Transform cameraTransform;

     [Header("終点の位置（空のGameObjectを割り当てる）")]
     public Transform endPointCamera;

     [Header("移動設定")]
     public float duration = 2f;
     public Ease easeType = Ease.InOutSine;*/
    public GameObject scoreText;
    public GameObject maxScoreText;
    public GameObject timeText;
    public GameObject enemyManager;
    public GameObject titleButton;
    public GameObject RetryButton;

    Ranking ranking;





    private enum HandState
    {
        Poi,
        Hand
    }

    private HandState currentHandState = HandState.Poi;






    enum Mode
    {
        Title, Game, Finish, Result
    };

    Mode mode = Mode.Title;
    // Start is called before the first frame update
    void Start()
    {
        ranking = FindObjectOfType<Ranking>();


    }

    // Update is called once per frame
    void Update()
    {

        switch (mode)
        {
            case Mode.Title:
                Title();
                break;
            case Mode.Game:
                Game();
                break;
            case Mode.Finish:
                Finish();

                break;
            case Mode.Result:
                Result();

                break;

        }

        HandleHandSwitch();
    }

    private void HandleHandSwitch()
    {
        if (mode != Mode.Game) return;  // ゲーム中以外は何もしない
        switch (currentHandState)
        {
            case HandState.Poi:

                poi.SetActive(true);
                hand.SetActive(false);
                rainBow.SetActive(false);

                if (mySlider.value >= mySlider.maxValue)
                {
                    // ここで切り替え前に位置を揃える
                    hand.transform.position = poi.transform.position;
                    handSlider.value = 100;
                    currentHandState = HandState.Hand;
                    Instantiate(feverPrefab, canvasTransform, false);

                }
                break;

            case HandState.Hand:
                poi.SetActive(false);
                hand.SetActive(true);
                rainBow.SetActive(true);


                if (handSlider.value <= handSlider.minValue)
                {

                    // 手を表示する前に位置をポイと揃える
                    poi.transform.position = hand.transform.position;
                    // 手 → ポイ に切り替えるときに即座にゲージを0にする
                    mySlider.value = 0;
                    currentHandState = HandState.Poi;
                }
                break;
        }
    }

    /*public void MoveCameraToEnd()
    {
        if (cameraTransform != null && endPointCamera != null)
        {
            Vector3 targetPos = endPointCamera.position;
            targetPos.z = cameraTransform.position.z; // Zは今の値を維持
            cameraTransform.DOMove(targetPos, duration).SetEase(easeType);

        }
    }*/






    public void Title()
    {
        finishTxt.SetActive(false);
        titleButton.SetActive(false);
        RetryButton.SetActive(false);

        mode = Mode.Game;

    }

    public void Game()
    {
        if (timeManager.countDown <= 0)
        {

            mode = Mode.Finish;

        }

    }
    public void OnTitleClicked()
    {
        SceneManager.LoadScene("Title");

    }

    public void OnRetryClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }





    public void Finish()
    {

        // 今までのコインを読み込み
        float savedCoin = PlayerPrefs.GetFloat("possessionCoin", 0f);

        // 今回の獲得コインを加算
        savedCoin += money.CurrentMoney;



        PlayerPrefs.SetFloat("possessionCoin", savedCoin);
        PlayerPrefs.Save();

        totalCoin = savedCoin;

        poi.SetActive(false);
        if (ranking != null)
        {
            ranking.SubmitScore(scoreManager.scoreMax_num);
            ranking.hasSubmittedScore = false; // スコア再送信を許可

        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // 普通のカーソル
        if (finishTxt != null)
        {
            finishTxt.SetActive(true);

        }

        titleButton.SetActive(true);
        RetryButton.SetActive(true);


        //Invoke("MoveCameraToEnd", 3f);
        mode = Mode.Result;
        //MoveCameraToEnd();
    }
    public void Result()
    {
        //scoreText.SetActive(false);
        //maxScoreText.SetActive(false);
        timeText.SetActive(false);
        poi.SetActive(false);
        hand.SetActive(false);

        enemyManager.SetActive(false);
        mySlider.gameObject.SetActive(false);
        //mode = Mode.Title;



    }


}
