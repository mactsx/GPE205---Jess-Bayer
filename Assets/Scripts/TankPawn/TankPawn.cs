using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    // Variables
    public Shooter shooter;
    public GameObject shellPrefab;
    public float fireForce;
    public float damageDone;
    public float lifeSpan;
    private float nextShootTime;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        shooter = GetComponent<Shooter>();
        nextShootTime = Time.time + fireRate;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Start();

        
    }

    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
    }

    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    // Have the tank pawn rotate towards a target
    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

 
    public override void Shoot()
    {
        if (Time.time >= nextShootTime)
        {
            shooter.Shoot(shellPrefab, fireForce, damageDone, lifeSpan);
            nextShootTime = Time.time + fireRate;                     

        }

    }

    public override void ResetNoise()
    {
        NoiseMaker noiseMaker = GetComponent<NoiseMaker>();
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = 0;
        }
    }

}
