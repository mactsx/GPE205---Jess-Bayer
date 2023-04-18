using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    // Variable to hold the Rigidbody Component
    private Rigidbody rigBody;

    // Start is called before the first frame update
    public override void Start()
    {
        // Get the Rigidbody component
        rigBody = GetComponent<Rigidbody>();
    }

    // Move the Tank in a direction by a speed
    public override void Move(Vector3 direction, float speed)
    {
        // Create a vec3 for movement that equals the normalized direction multiplied by the incoming speed variable * by the change in time
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rigBody.MovePosition(rigBody.position + moveVector);
    }

    // Rotate the tank based on the rotate speed
    public override void Rotate(float rotateSpeed)
    {
        transform.Rotate(0, (rotateSpeed * Time.deltaTime), 0);
    }

}
