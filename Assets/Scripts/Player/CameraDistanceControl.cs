using Unity.VisualScripting;
using UnityEngine;

public class CameraDistanceControl : MonoBehaviour
{
    public float zoomSpeed = 0.5f; // Velocidade de zoom da c�mera
    public float minFov = 0.5f; // Campo de vis�o m�nimo da c�mera
    public float maxFov = 10f; // Campo de vis�o m�ximo da c�mera

    public Camera Cam;

    private void Start()
    {
        Cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Verifica se o usu�rio pressionou o bot�o "+" para aproximar a c�mera
        if (Input.GetKeyDown(KeyCode.K))
        {
            ZoomCamera(-zoomSpeed);
        }

        // Verifica se o usu�rio pressionou o bot�o "-" para afastar a c�mera
        if (Input.GetKeyDown(KeyCode.L))
        {
            ZoomCamera(zoomSpeed);
        }
    }

    // Fun��o para ajustar o zoom da c�mera
    void ZoomCamera(float zoomChange)
    {
        Cam.orthographicSize += zoomChange;
    }
}
