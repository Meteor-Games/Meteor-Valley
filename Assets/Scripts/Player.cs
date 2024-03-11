using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Windows;
using Unity.Netcode;

public class Player : Entity 
{
    public Animator playerAnimator;
    float input_x = 0;
    float input_y = 0;
    
    [SerializeField]
    private bool isWalking = false;
    protected void Start()
    {
        base.Start();
        isWalking = false;
        this.entityData.Value.pickItens = true;
    }
    public override void OnNetworkSpawn()
    {
        if (!IsLocalPlayer)
        {
            Destroy(this);
            return;
        }
    }

    private new void Update()
    {
        base.Update();

        input_x = UnityEngine.Input.GetAxisRaw("Horizontal");
        input_y = UnityEngine.Input.GetAxisRaw("Vertical");
        isWalking = (input_x != 0 || input_y != 0);

        if (isWalking)
        {
            var move = new Vector3(input_x, input_y, 0).normalized;
            transform.position += this.EntityData.moveSpeed * Time.deltaTime * move;
            playerAnimator.SetFloat("input_x", input_x);
            playerAnimator.SetFloat("input_y", input_y);
        }

        playerAnimator.SetBool("isWalking", isWalking);

        if (UnityEngine.Input.GetButton("Fire1"))
        {
            playerAnimator.SetTrigger("atack");
        }
            
        
    }

}
