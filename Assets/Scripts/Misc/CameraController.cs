using System;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    public void MoveCameraTo(Vector3 position)
    {
        int x = ((int) (Math.Round(position.x / 32))) * 32;
        int y = ((int) (Math.Round(position.y / 32))) * 32;
        mainCamera.transform.position = new Vector3(x, y, mainCamera.transform.position.z);
    }
}
