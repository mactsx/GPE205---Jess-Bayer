using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
