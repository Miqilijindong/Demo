using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController_Scaler : MonoBehaviour
{
    public static Camera gameCamera
    {
        get; private set;
    }

    public static float cameraHeightSize
    {
        get; private set;
    }

    public static float cameraWidthSize
    {
        get; private set;
    }

    private void Awake()
    {
        gameCamera = gameObject.GetComponent<Camera>();
        SetCameraSize();
    }

    private void SetCameraSize()
    {
        cameraHeightSize = Screen.height / 100.0f;
        gameCamera.orthographicSize = cameraHeightSize / 2.0f;

        float aspect = (float)Screen.height / (float)Screen.width;
        cameraWidthSize = cameraHeightSize / aspect;
    }
}