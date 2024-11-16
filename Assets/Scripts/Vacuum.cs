using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Vacuum : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI equipmentCounter;
    Queue<ObjectScript> suckedObjects = new Queue<ObjectScript>();
    [SerializeField] int maxObjectCap = 5;

    public static UnityEvent<int> OnObjectSucked = new UnityEvent<int>();
    public static UnityEvent<int> OnObjectDropped = new UnityEvent<int>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            if (maxObjectCap == suckedObjects.Count) return;
            Debug.Log("obiekt!!");
            ObjectScript newObject = other.gameObject.GetComponent<ObjectScript>();
            Debug.Log($"Sucking object with ID: {newObject.objectID}, from era: {newObject.currentEra}, original era: {newObject.originalEra}");
            suckedObjects.Enqueue(newObject);
            other.gameObject.SetActive(false);

            equipmentCounter.text = $"{suckedObjects.Count}/{maxObjectCap}";
            OnObjectSucked.Invoke(newObject.objectID);
        }
    }

    public void DropObjects()
    {
        if (suckedObjects.Count == 0) return;
        ObjectScript item = suckedObjects.Dequeue();
        item.gameObject.SetActive(true);
        item.gameObject.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        Debug.Log(item.objectID);
        gameObject.SetActive(false);

        equipmentCounter.text = $"{suckedObjects.Count}/{maxObjectCap}";
        OnObjectDropped.Invoke(item.objectID);
    }
}
