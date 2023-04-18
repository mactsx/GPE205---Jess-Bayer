using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
