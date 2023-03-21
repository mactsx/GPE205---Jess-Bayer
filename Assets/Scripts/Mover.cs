using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void Start();

    // Abstract Move Function that sends in a direction as a vector and a speed variable
    public abstract void Move(Vector3 direction, float speed);

    // Function to rotate the pawn
    public abstract void Rotate(float rotateSpeed);

}
