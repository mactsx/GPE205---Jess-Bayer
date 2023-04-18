using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{
    public float healthToAdd;
    public override void Apply(PowerupManager target)
    {
        // Get their health component
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            // Since no one is directly causing the healing, they are healing themselves
            targetHealth.Heal(healthToAdd, target.GetComponent<Pawn>());
        }
    }

    public override void Remove(PowerupManager target)
    {
        // Get their health component
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            // Since no one is directly causing the healing, they are healing themselves
            targetHealth.TakeDamage(healthToAdd, target.GetComponent<Pawn>());
        }
    }
}
