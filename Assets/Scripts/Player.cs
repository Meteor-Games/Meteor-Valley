using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator playerAnimator;
    float imput_x = 0;
    float imput_y = 0;
    bool isWalking = false;
    public float speed = 2.5f;

    private void Start()
    {
        isWalking = false;
    }


    private void Update()
    {
        imput_x = Input.GetAxisRaw("Horizontal");
        imput_y= Input.GetAxisRaw("Vertical");
        isWalking = (imput_x != 0 || imput_y != 0);

        if (isWalking)
        {
            var move = new Vector3(imput_x, imput_y, 0).normalized;
            transform.position += move * speed * Time.deltaTime;
        }
    }
}