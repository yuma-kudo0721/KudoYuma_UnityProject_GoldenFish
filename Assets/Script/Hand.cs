using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    Animator anim;
    public Slider handSlider;
    [SerializeField] float decreaseSpeed;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 2.0f; // 再生速度を2倍に

    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            anim.SetBool("Hand", true);

        }
        else
        {
            anim.SetBool("Hand", false);
        }

        handSlider.value -= Time.deltaTime * decreaseSpeed;






    }
}
