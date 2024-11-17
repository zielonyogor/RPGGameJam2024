using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Vacuum : MonoBehaviour
{
    [SerializeField] private HUDController hudController;
    Queue<ObjectScript> suckedObjects = new Queue<ObjectScript>();

    private BoxCollider2D boxCollider2D;

    public static UnityEvent<int> OnObjectSucked = new UnityEvent<int>();
    public static UnityEvent<int> OnObjectDropped = new UnityEvent<int>();

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
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
        float attractionDuration = 0.2f; // Time in seconds for the object to move
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
    }


    public void DropObjects()
    {
        if (suckedObjects.Count == 0) return;
        ObjectScript item = suckedObjects.Dequeue();
        item.gameObject.SetActive(true);
        item.gameObject.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        // Debug.Log(item.objectID);

        hudController.equipment = suckedObjects.Count;
        OnObjectDropped.Invoke(item.objectID);
    }
}
