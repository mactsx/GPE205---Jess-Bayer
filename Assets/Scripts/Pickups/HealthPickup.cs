using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public HealthPowerup powerup;
    public float rotationAmt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationAmt, 0, Space.World);
    }

    public void OnTriggerEnter(Collider other)
    {
        // Try to get the other object's powerupController
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        // If there is a powerup manager
        if (powerupManager != null)
        {
            // Add the powerup
            powerupManager.Add(powerup);

            // Destroy itself
            Destroy(gameObject);
        }

    }

}
