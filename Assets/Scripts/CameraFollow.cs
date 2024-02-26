using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Transform do jogador
    public Transform followObject; // Transform do objeto no slot

    void LateUpdate()
    {
        // Se houver um objeto no slot, siga esse objeto
        if (followObject != null)
        {
            FollowObject(followObject);
        }
        else if (player != null) // Sen�o, siga o jogador
        {
            FollowObject(player);
        }
    }

    // Fun��o para fazer a c�mera seguir um objeto
    void FollowObject(Transform target)
    {
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z; // Mantenha a mesma dist�ncia da c�mera ao objeto na dire��o z
        transform.position = targetPosition;
    }

    // Fun��o para definir o jogador como o objeto a ser seguido
    public void SetPlayerAsTarget(Transform newPlayer)
    {
        player = newPlayer;
    }

    // Fun��o para definir o objeto no slot como o objeto a ser seguido
    public void SetObjectSlotAsTarget(Transform newObjectSlot)
    {
        followObject = newObjectSlot;
    }
}
