using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Vacuum : MonoBehaviour
{
    [SerializeField] private HUDController hudController;
    [SerializeField] VacuumSounds vacuumSounds;
    Queue<ObjectScript> suckedObjects = new Queue<ObjectScript>();

    private BoxCollider2D boxCollider2D;

    public static UnityEvent<int> OnObjectSucked = new UnityEvent<int>();
    public static UnityEvent<int> OnObjectDropped = new UnityEvent<int>();


    public PlayerManager playerManager;
    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        if (playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("skibidi");
        if (other.gameObject.CompareTag("Object"))
        {
            if (suckedObjects.Count == hudController.maxEquipment) return;
            // Debug.Log("obiekt!!");
            ObjectScript newObject = other.gameObject.GetComponent<ObjectScript>();
            // Debug.Log($"Sucking object with ID: {newObject.objectID}, from era: {newObject.currentEra}, original era: {newObject.originalEra}");

            suckedObjects.Enqueue(newObject);

            vacuumSounds.PlaySingleSuccSound();
            StartCoroutine(AttractObjectToPlayer(other.gameObject, newObject));

            hudController.equipment = suckedObjects.Count;
            OnObjectSucked.Invoke(newObject.objectID);
        }
    }


    public void SuckObjects()
    {
        boxCollider2D.enabled = true;
    }

    public void HideVacuum()
    {
        boxCollider2D.enabled = false;
    }

    private IEnumerator<object> AttractObjectToPlayer(GameObject obj, ObjectScript newObject)
    {
        float attractionDuration = 0.4f; // Time in seconds for the object to move
        float elapsedTime = 0f;

        Vector3 startPosition = obj.transform.position;
        Vector3 targetPosition = transform.position; // Position of the player

        // Smoothly move the object towards the player
        while (elapsedTime < attractionDuration)
        {
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / attractionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure object is exactly at the target position
        obj.transform.position = targetPosition;

        // Disable the object
        obj.SetActive(false);
        vacuumSounds.PlayPopSound();
    }


    public void DropObjects()
    {
        if (suckedObjects.Count == 0) return;

        ObjectScript item = suckedObjects.Dequeue();
        item.gameObject.SetActive(true);

        // Set initial position slightly away from the player
        Vector3 initialPosition = transform.position + new Vector3(0.5f, 0.5f, 0); ;
        item.gameObject.transform.position = initialPosition;

        // Calculate the direction vector away from the character
        Vector3 moveDirection = playerManager.moveDir;
        float moveDistance = 10f; // Distance to move away
        float moveDuration = 0.5f; // Duration of the movement

        // Start coroutine to move the object away
        StartCoroutine(MoveObjectAway(item.gameObject, initialPosition, moveDirection, moveDistance, moveDuration));


        item.gameObject.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        
        vacuumSounds.PlayPopSound();

        hudController.equipment = suckedObjects.Count;
        OnObjectDropped.Invoke(item.objectID);
    }
    
    private IEnumerator<object> MoveObjectAway(GameObject obj, Vector3 startPosition, Vector3 direction, float distance, float duration)

    {
        float elapsedTime = 0f;
        Vector3 targetPosition = startPosition + direction * distance;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float easedT = 1 - Mathf.Pow(1 - t, 2);  // This is a quadratic ease-out function

            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, easedT);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object is exactly at the target position
        obj.transform.position = targetPosition;
    }


}



