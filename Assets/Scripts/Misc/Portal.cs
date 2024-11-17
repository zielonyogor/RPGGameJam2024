using System;
using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Tooltip("Teleport - a child of a second portal")]
    [SerializeField] Transform teleportPoint;
    [SerializeField] AudioSource portalSound;
    [SerializeField] Room target;

    public float teleportFreezeTime = 0.5f;
    private bool isFreezeCoroutineExecuting = false;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            playerManager.movementEnabled = false;
            portalSound.Play();
            StartCoroutine(ExecuteDelayed(teleportFreezeTime, () =>
            {
                player.transform.position = teleportPoint.position;
                playerManager.movementEnabled = true;
                playerManager.currentTerrain = Rooms.GetTerrain(target);
                playerManager.SetVision(Rooms.GetVisionMode(target));
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
