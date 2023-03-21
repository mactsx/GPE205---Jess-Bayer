using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firepointTransform;
    public float shootVolumeDistance;

    public override void Start()
    {

    }

    public override void Update()
    {

    }

    public override void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifeSpan)
    {
        // Instantiate the projectile
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation);
        // Get a noisemaker
        NoiseMaker noiseMaker = GetComponent<NoiseMaker>();

        // If there is a noisemaker, set the noisemaker's distance to be whatever is set
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = shootVolumeDistance;
        }

        // Get the DamageOnHit
        DamageOnHit dmgOnHit = newShell.GetComponent<DamageOnHit>();

        if (dmgOnHit != null)
        {
            dmgOnHit.damageDone = damageDone;
            dmgOnHit.owner = GetComponent<Pawn>();
        }

        Rigidbody rigBody = newShell.GetComponent<Rigidbody>();

        if (rigBody != null)
        {
            rigBody.AddForce(firepointTransform.forward * fireForce);
        }

        Destroy(newShell, lifeSpan);
                
    }


}
