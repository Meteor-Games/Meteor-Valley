using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    public Animator playerAnimator;
    float input_x = 0;
    float input_y = 0;
    bool isWalking = false;
    public float speed = 2.5f;

    private void Start()
    {
        isWalking = false;
    }


    private void Update()
    {
        input_x = UnityEngine.Input.GetAxisRaw("Horizontal");
        input_y = UnityEngine.Input.GetAxisRaw("Vertical");
        isWalking = (input_x != 0 || input_y != 0);

        if (isWalking)
        {
            var move = new Vector3(input_x, input_y, 0).normalized;
            transform.position += move * speed * Time.deltaTime;
            playerAnimator.SetFloat("input_x", input_x);
            playerAnimator.SetFloat("input_y", input_y);
        }

        playerAnimator.SetBool("isWalking", isWalking);

        if (UnityEngine.Input.GetButton("Fire1"))
            playerAnimator.SetTrigger("atack");
    }

}
