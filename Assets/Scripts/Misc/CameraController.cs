using System;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    [SerializeField] float PositionCorrectionFactor = 10f;

    [SerializeField] Camera mainCamera;

    public void MoveCameraTo(Vector3 position)
    {
        int x = ((int) (Math.Round(position.x / Rooms.RoomSize))) * Rooms.RoomSize;
        int y = ((int) (Math.Round(position.y / Rooms.RoomSize))) * Rooms.RoomSize;
        float correctionX = PositionCorrectionFactor * (position.x - x) / Rooms.RoomSize;
        float correctionY = PositionCorrectionFactor * (position.y - y) / Rooms.RoomSize;
        mainCamera.transform.position = new Vector3(x + correctionX, y + correctionY, mainCamera.transform.position.z);
    }
}
