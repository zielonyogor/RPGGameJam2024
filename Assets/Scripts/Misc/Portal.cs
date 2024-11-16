using UnityEngine;

public class Portal : MonoBehaviour
{
    [Tooltip("Teleport - a child of a second portal")]
    [SerializeField] Transform teleportPoint;

    [SerializeField] TerrainType terrainAtTheOtherSide;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = teleportPoint.position;
            PlayerManager player = other.GetComponent<PlayerManager>();
            player.currentTerrain = terrainAtTheOtherSide;
        }
    }
}
