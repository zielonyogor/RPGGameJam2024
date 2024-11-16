using UnityEngine;

public class Portal : MonoBehaviour
{
    [Tooltip("Teleport - a child of a second portal")]
    [SerializeField] Transform teleportPoint;

    [SerializeField] TerrainType terrainAtTheOtherSide;

    [SerializeField] CameraController cameraController;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            player.transform.position = teleportPoint.position;
            cameraController.MoveCameraTo(teleportPoint.position);
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            playerManager.currentTerrain = terrainAtTheOtherSide;
        }
    }
}
