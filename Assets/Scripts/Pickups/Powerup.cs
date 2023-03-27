using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup
{
    public float duration;
    public bool isPermanent;
    

    public abstract void Apply(PowerupManager target);
    public abstract void Remove(PowerupManager target);
}

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

[System.Serializable]
public class SpeedPowerup : Powerup
{
    public float speedPctToAdd;

    public override void Apply(PowerupManager target)
    {
        TankPawn targetSpeed = target.GetComponent<TankPawn>();
        if (targetSpeed != null)
        {
            targetSpeed.moveSpeed *= ((speedPctToAdd / 100) + 1);
        }
    }
    public override void Remove(PowerupManager target)
    {
        TankPawn targetSpeed = target.GetComponent<TankPawn>();
        if (targetSpeed != null)
        {
            targetSpeed.moveSpeed /= ((speedPctToAdd / 100) + 1);
        }
    }

}

[System.Serializable]
public class DamagePowerup : Powerup
{
    public float dmgMultiplier;

    public override void Apply(PowerupManager target)
    {
        TankPawn targetSpeed = target.GetComponent<TankPawn>();
        if (targetSpeed != null)
        {
            targetSpeed.damageDone *= dmgMultiplier;
        }
    }
    public override void Remove(PowerupManager target)
    {
        TankPawn targetSpeed = target.GetComponent<TankPawn>();
        if (targetSpeed != null)
        {
            targetSpeed.damageDone /= dmgMultiplier;
        }
    }

}
