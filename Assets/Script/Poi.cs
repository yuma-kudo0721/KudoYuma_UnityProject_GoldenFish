using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Poi : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float poiSpeed;

    public GameObject defeatFish;
    public GameObject defeDatu;
    public GameObject hand;

    public int destoroyCount = 0;

    public ScoreManager scoreManager;
    public TimeManager timeManager;
    public Money money;

    private bool isInTrigger = false;
    private Collider2D targetFish = null;

    Animator anim;

    bool isbroken = false;

    [SerializeField] float breakTime = 2f;
    [SerializeField] float breakTimeStart;
    [SerializeField] float decreaseSpeedPoi;

    public Slider mySlider;

    public GameObject timeTextPrefab;
    public Transform worldCanvas;

    public List<Collider2D> fishInRange = new List<Collider2D>();
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip poiSE;
    [SerializeField] private AudioClip goldfishSE;

    private Vector2 moveInput;

    public enum ControlMode
    {
        Keyboard,
        Mouse
    }
    public ControlMode currentMode = ControlMode.Mouse;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // --- 操作モード自動切り替え ---
        if (Mouse.current != null && Mouse.current.delta.ReadValue() != Vector2.zero)
            currentMode = ControlMode.Mouse;
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            currentMode = ControlMode.Keyboard;

        // --- 移動処理 ---
        if (currentMode == ControlMode.Keyboard)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // 普通のカーソル

            Vector2 keyboardInput = Vector2.zero;
            if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) keyboardInput.x -= 1;
            if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) keyboardInput.x += 1;
            if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed) keyboardInput.y -= 1;
            if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed) keyboardInput.y += 1;

            if (keyboardInput.sqrMagnitude > 1f) keyboardInput = keyboardInput.normalized;

            Vector2 combinedInput = moveInput + keyboardInput;
            if (combinedInput.sqrMagnitude > 1f) combinedInput = combinedInput.normalized;

            rb.velocity = combinedInput * poiSpeed;
        }
        else if (currentMode == ControlMode.Mouse)
        {
            MouseMode(); // マウス追従
            rb.velocity = Vector2.zero;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined; // 画面内で動く
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // 普通のカーソル
        }

        // --- スライダー減少処理 ---
        if (mySlider != null)
            mySlider.value -= Time.deltaTime * decreaseSpeedPoi;

        // --- 壊れ状態の処理 ---
        if (isbroken)
        {
            breakTime -= Time.deltaTime;
            if (breakTime <= 0f)
            {
                isbroken = false;
                anim.SetBool("Break", false);
                breakTime = breakTimeStart;
            }
        }

        // --- 入力判定 ---
        bool inputPressed = false;
        if (currentMode == ControlMode.Keyboard)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
                inputPressed = true;
        }
        else if (currentMode == ControlMode.Mouse)
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
                inputPressed = true;
        }

        // --- 入力があった場合の処理 ---
        if (inputPressed)
        {
            if (!isbroken && isInTrigger && targetFish != null)
            {
                IFish fish = targetFish.GetComponent<IFish>();
                if (fish != null)
                {
                    IncreaseSlider(10f);
                    timeManager.AddTime(fish.GetTime());
                    money.AddMoney(fish.GetMoney());

                    GameObject textObj = Instantiate(timeTextPrefab, worldCanvas);
                    textObj.transform.position = targetFish.transform.position + new Vector3(0, 2f, 0);

                    fish.OnDefeated();
                    scoreManager.AddScore(fish.GetScore());
                }
            }
            else
            {
                isbroken = true;
                anim.SetBool("Break", true);
            }
        }
    }

    // マウスモード時の追従処理
    public void MouseMode()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;
    }

    // Input System の Move アクションに接続
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void IncreaseSlider(float amount)
    {
        mySlider.value += amount;
        if (mySlider.value > mySlider.maxValue)
            mySlider.value = mySlider.maxValue;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<IFish>() != null)
        {
            if (!fishInRange.Contains(other))
                fishInRange.Add(other);

            targetFish = GetClosestFish();
            isInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (fishInRange.Contains(other))
            fishInRange.Remove(other);

        if (fishInRange.Count == 0)
        {
            isInTrigger = false;
            targetFish = null;
        }
        else
        {
            targetFish = GetClosestFish();
        }
    }

    Collider2D GetClosestFish()
    {
        Collider2D closest = null;
        float minDist = float.MaxValue;

        foreach (var fish in fishInRange)
        {
            if (fish == null) continue;
            float dist = Vector2.Distance(transform.position, fish.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = fish;
            }
        }
        return closest;
    }
}
