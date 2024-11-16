using System.Collections.Generic;
using UnityEngine;

public class Vaccum : MonoBehaviour
{
    List<ObjectScript> suckedObjects = new List<ObjectScript>();
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Object"))
        {
            Debug.Log("obiekt!!");
            ObjectScript newObject = other.gameObject.GetComponent<ObjectScript>();
            suckedObjects.Add(newObject);
            other.gameObject.SetActive(false);
        }
    }

    public void DropObjects()
    {
        Debug.Log("helo");
        foreach (ObjectScript item in suckedObjects)
        {
            item.gameObject.SetActive(true);
            item.currentEra = "tu puzniej trzeba zmienic na current era";
            item.gameObject.transform.position = transform.position;
        }
        gameObject.SetActive(false);
    }
}
