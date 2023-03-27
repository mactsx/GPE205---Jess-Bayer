using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{    
    // List of all active powerups
    public List<Powerup> powerups;
    // List of powerups to be removed
    private List<Powerup> removedPowerupQueue;

    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<Powerup>();
        removedPowerupQueue = new List<Powerup>();
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease all powerup timers
        DecrementPowerupTimers();
    }

    private void LateUpdate()
    {
        ApplyRemovePowerupsQueue();
    }

    public void Add (Powerup powerupToAdd)
    {
        // Apply the powerup
        powerupToAdd.Apply(this);
        // Add it to a list
        powerups.Add(powerupToAdd);
    }

    public void DecrementPowerupTimers()
    {
        // For each powerup in the list,
        foreach (Powerup powerup in powerups)
        {
            // If they are not a permanent powerup
            if (!powerup.isPermanent)
            {
                // Decrease its duration
                powerup.duration -= Time.deltaTime;

                // And if the duration is over, remove the powerup
                if (powerup.duration <= 0)
                {
                    Remove(powerup);
                }
            }
        }
    }

    public void Remove(Powerup powerupToRemove)
    {
        // Remove powerup
        powerupToRemove.Remove(this);
        // Add it to the remove queue list
        removedPowerupQueue.Add(powerupToRemove);
    }

    public void ApplyRemovePowerupsQueue()
    {
        // Remove the powerups after iterating through the active powerups
        foreach (Powerup powerup in removedPowerupQueue)
        {
            powerups.Remove(powerup);
        }
        // Reset the temporary list
        removedPowerupQueue.Clear();
    }


}
