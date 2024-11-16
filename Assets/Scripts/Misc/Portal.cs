using System;
using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Tooltip("Teleport - a child of a second portal")]
    [SerializeField] Transform teleportPoint;
    [SerializeField] TerrainType terrainAtTheOtherSide;

    public float teleportFreezeTime = 1f;
    private bool isFreezeCoroutineExecuting = false;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            playerManager.movementEnabled = false;
            StartCoroutine(ExecuteDelayed(teleportFreezeTime, () =>
            {            
                player.transform.position = teleportPoint.position;
                playerManager.movementEnabled = true;
                playerManager.currentTerrain = terrainAtTheOtherSide;
            }
            ));
        }
    }

    private IEnumerator ExecuteDelayed(float time, Action task)
    {
        if (isFreezeCoroutineExecuting) yield break;
        isFreezeCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        isFreezeCoroutineExecuting = false;
    }
}
