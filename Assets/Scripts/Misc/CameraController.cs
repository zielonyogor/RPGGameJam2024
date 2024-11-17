using System;
using UnityEngine;

public enum VisionMode
{
    Default, // clear any filters
    PortalTransition, // black semi-transparent overlay
    Sepia, // sepia filter
    Cyberpunk // purple filter
}

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    private static readonly Color sepia = new Color(0.5f, 0.5f, 0.2f);
    private static readonly Color cyberpunk = new Color(0.2f, 0.5f, 0.5f);

    public void MoveCameraTo(Vector3 position)
    {
        int x = ((int) (Math.Round(position.x / 32))) * 32;
        int y = ((int) (Math.Round(position.y / 32))) * 32;
        mainCamera.transform.position = new Vector3(x, y, mainCamera.transform.position.z);
    }

    public void SetFilter(VisionMode filter)
    {
        switch (filter)
        {
            case VisionMode.Default:
                mainCamera.clearFlags = CameraClearFlags.SolidColor;
                mainCamera.backgroundColor = Color.black;
                mainCamera.cullingMask = -1;
                break;
            case VisionMode.PortalTransition:
                mainCamera.clearFlags = CameraClearFlags.SolidColor;
                mainCamera.backgroundColor = Color.black;
                mainCamera.cullingMask = 0;
                break;
            case VisionMode.Sepia:
                mainCamera.clearFlags = CameraClearFlags.SolidColor;
                mainCamera.backgroundColor = CameraController.sepia;
                mainCamera.cullingMask = -1;
                break;
            case VisionMode.Cyberpunk:
                mainCamera.clearFlags = CameraClearFlags.SolidColor;
                mainCamera.backgroundColor = CameraController.cyberpunk;
                mainCamera.cullingMask = -1;
                break;
        }
    }
}
