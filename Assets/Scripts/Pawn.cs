using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    // Variables
    // For move and turning speed
    // Public floats so they can be accessed
    public float moveSpeed;
    public float turnSpeed;
    public float fireRate;

    // Add a variable to hold a mover
    public Mover mover;


    // Start is called before the first frame update
    public virtual void Start()
    {
        // Get the mover component and set it to the mover variable
        mover = GetComponent<Mover>();        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }


    // Functions that tell a pawn they can move and rotate
    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();
    public abstract void RotateTowards(Vector3 targetPosition);
    public abstract void ResetNoise();

}
