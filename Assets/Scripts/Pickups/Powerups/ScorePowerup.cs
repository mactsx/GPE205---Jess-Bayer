using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScorePowerup : Powerup
{
    public int ScoreToAdd;

    public override void Apply(PowerupManager target)
    {
        TankPawn targetPawn = target.GetComponent<TankPawn>();
        if (targetPawn != null)
        {
            targetPawn.controller.AddToScore(ScoreToAdd);
        }
    }

    public override void Remove(PowerupManager target)
    { }

}
