using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

//disable entire Game Object if you don't want a lot of objects in scene
public class ObjectManager : MonoBehaviour
{
    [Header("Counter")]
    [SerializeField] HUDController counterController;

    [Header("Object to spawn")]
    [SerializeField] List<GameObject> objectPrefabs;

    [Header("Rooms")]
    [SerializeField] Transform pastRoom;
    [SerializeField] Transform presentRoom;
    [SerializeField] Transform futureRoom;

    [Header("Spawn time range")]
    [SerializeField] float minSpawnTime = 0.5f;
    [SerializeField] float maxSpawnTime = 5f;
    private float nextSpawnTime = 0;
    private float lastSpawnTime = 0;

    private List<ObjectScript> spawnedObjects = new List<ObjectScript>();

    void Start()
    {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        Vacuum.OnObjectSucked.AddListener(HandleObjectSucked);
        Vacuum.OnObjectDropped.AddListener(HandleObjectDropped);
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > nextSpawnTime)
        {
            lastSpawnTime = Time.time;
            nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            int eraToSpawn = Random.Range(0, 3), currentObjectEra = Random.Range(0, 3); //1-past, 2-present, 3-future

            Vector3 randomPosition = new Vector3(Random.Range(-12, 12), Random.Range(-12, 12), 0); // randomize position here
            Transform parentPosition;
            switch (eraToSpawn)
            {
                case 0:
                    parentPosition = pastRoom;
                    break;
                case 1:
                    parentPosition = presentRoom;
                    break;
                default:
                    parentPosition = futureRoom;
                    break;
            }
            randomPosition += parentPosition.position; //adjust to a room

            GameObject objectPrefab = objectPrefabs[currentObjectEra * 2 + Random.Range(0, 2)]; //waz (+1) lub mis (+0)
            GameObject newObject = Instantiate(objectPrefab, randomPosition, Quaternion.identity, parentPosition);

            ObjectScript objectScript = newObject.GetComponent<ObjectScript>();
            objectScript.originalEra = (Era) currentObjectEra;
            if (objectScript.originalEra != (Era) eraToSpawn)
            {
                objectScript.currentEra = (Era) eraToSpawn;
                ++counterController.anomalies;
            }
            objectScript.currentEra = (Era) eraToSpawn;
            objectScript.objectID = spawnedObjects.Count;
            spawnedObjects.Add(objectScript);
        }
    }

    private void HandleObjectSucked(int objectID)
    {
        foreach (ObjectScript obj in spawnedObjects) //goofy ass ale nam wystarczy i guess
        {
            if (objectID == obj.objectID)
            {
                if (obj.currentEra != obj.originalEra) --counterController.anomalies;
                break;
            }
        }
    }

    private void HandleObjectDropped(int objectID)
    {
        foreach (ObjectScript obj in spawnedObjects)
        {
            if (objectID == obj.objectID)
            {
                Era newEra;
                //na razie tak sprawdzam w jakiej epoce jest
                // if (obj.gameObject.transform.position.y > -48)
                if (Rooms.IsInsideRoom(Room.Past, obj.gameObject.transform.position))
                {
                    newEra = Era.Past;
                }
                // else if (obj.gameObject.transform.position.y > -80)
                else if (Rooms.IsInsideRoom(Room.Future, obj.gameObject.transform.position))
                {
                    newEra = Era.Future;
                }
                else
                // else if (Rooms.IsInsideRoom(Room.Present, obj.gameObject.transform.position))
                {
                    newEra = Era.Present;
                }

                obj.currentEra = newEra;
                if (obj.currentEra != obj.originalEra)
                {
                    ++counterController.anomalies;
                }
                // Debug.Log($"Object ({obj.objectID}) was dropped at {newEra}, previous era {obj.currentEra}, original era {obj.originalEra}");
                break;
            }
        }
    }
}
