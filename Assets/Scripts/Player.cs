using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Windows;
using Unity.Netcode;

public class PlayerMovement : Entity 
{
    public Animator playerAnimator;
    float input_x = 0;
    float input_y = 0;
    
    [SerializeField]
    private bool isWalking = false;

    private void Start()
    {

        isWalking = false;
    }

    public override void OnNetworkSpawn()
    {
        
        if (!IsLocalPlayer)
        {
            Destroy(this);
            return;
        }
        Debug.Log("is_player");
    }

    private void Update()
    {
        input_x = UnityEngine.Input.GetAxisRaw("Horizontal");
        input_y = UnityEngine.Input.GetAxisRaw("Vertical");
        isWalking = (input_x != 0 || input_y != 0);

        if (isWalking)
        {
            var move = new Vector3(input_x, input_y, 0).normalized;
            transform.position += this.entityData.Value.moveSpeed * Time.deltaTime * move;
            playerAnimator.SetFloat("input_x", input_x);
            playerAnimator.SetFloat("input_y", input_y);
        }

        playerAnimator.SetBool("isWalking", isWalking);

        if (UnityEngine.Input.GetButton("Fire1"))
            playerAnimator.SetTrigger("atack");
        
    }

}
