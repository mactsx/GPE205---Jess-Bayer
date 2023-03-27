using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    private GameObject spawnedPickup;
    public float spawnDelay;
    private float nextSpawnTime;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;    
    }

    // Update is called once per frame
    void Update()
    {
        // If there is not a spawned pickup
        if (spawnedPickup == null)
        {
            // And if it is time to spawn another pickup
            if (Time.time > nextSpawnTime)
            {
                // Spawn the pickup and reset the timer
                spawnedPickup = Instantiate(pickupPrefab, transform.position, transform.rotation);

                spawnedPickup.transform.parent = transform;

                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            // Otherwise the object is still there and the timer is reset
            nextSpawnTime = Time.time + spawnDelay;
        }

    }
}
