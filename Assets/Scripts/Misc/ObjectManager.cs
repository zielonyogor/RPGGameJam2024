using UnityEngine;

//disable entire Game Object if you don't want a lot of objects in scene
public class ObjectManager : MonoBehaviour
{
    [Header("Object to spawn")]
    [SerializeField] GameObject objectPrefab;

    [Header("Spawn time range")]
    [SerializeField] float minSpawnTime = 0.5f;
    [SerializeField] float maxSpawnTime = 5f;
    private float nextSpawnTime = 0;
    private float lastSpawnTime = 0;

    void Start()
    {
        nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > nextSpawnTime)
        {
            lastSpawnTime = Time.time;
            nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

            Instantiate(objectPrefab);
            //randomize position here
        }
    }
}
