using Unity.VisualScripting;
using UnityEngine;

public class CameraDistanceControl : MonoBehaviour
{
    public float zoomSpeed = 0.5f; // Velocidade de zoom da câmera
    public float minFov = 0.5f; // Campo de visão mínimo da câmera
    public float maxFov = 10f; // Campo de visão máximo da câmera

    public Camera Cam;

    private void Start()
    {
        Cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Verifica se o usuário pressionou o botão "+" para aproximar a câmera
        if (Input.GetKeyDown(KeyCode.K))
        {
            ZoomCamera(-zoomSpeed);
        }

        // Verifica se o usuário pressionou o botão "-" para afastar a câmera
        if (Input.GetKeyDown(KeyCode.L))
        {
            ZoomCamera(zoomSpeed);
        }
    }

    // Função para ajustar o zoom da câmera
    void ZoomCamera(float zoomChange)
    {
        Cam.orthographicSize += zoomChange;
    }
}
