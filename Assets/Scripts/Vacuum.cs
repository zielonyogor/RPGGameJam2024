using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vacuum : MonoBehaviour
{
    List<ObjectScript> suckedObjects = new List<ObjectScript>();
    public static UnityEvent<int> OnObjectSucked = new UnityEvent<int>();
    public static UnityEvent<int> OnObjectDropped = new UnityEvent<int>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            Debug.Log("obiekt!!");
            ObjectScript newObject = other.gameObject.GetComponent<ObjectScript>();
            Debug.Log($"Sucking object with ID: {newObject.objectID}, from era: {newObject.currentEra}, original era: {newObject.originalEra}");
            suckedObjects.Add(newObject);
            other.gameObject.SetActive(false);
            OnObjectSucked.Invoke(newObject.objectID);
        }
    }

    public void DropObjects()
    {
        foreach (ObjectScript item in suckedObjects) //moze zmienic na zrzucanie po jednym???
        {
            item.gameObject.SetActive(true);
            item.gameObject.transform.position = transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
            OnObjectDropped.Invoke(item.objectID);
        }
        suckedObjects.Clear();
        gameObject.SetActive(false);
    }
}
