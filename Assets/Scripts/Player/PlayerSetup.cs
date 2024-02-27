using Unity.Netcode;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    private void Start()
    {
        if (IsLocalPlayer)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                CameraFollow cameraFollow = mainCamera.GetComponent<CameraFollow>();
                if (cameraFollow != null)
                {
                    cameraFollow.player = gameObject.transform;
                }
            }
        }
    }
}
