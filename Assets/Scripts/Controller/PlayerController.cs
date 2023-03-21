using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerController : Controller
{
    // Create movement keys
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode shootKey;

    // Start is called before the first frame update
    public override void Start()
    {
        // If there is a game manager instance
        if (GameManager.instance != null)
        {
            // And if there is a list of player
            if (GameManager.instance.players != null)
            {
                // Add this player controller to the list
                GameManager.instance.players.Add(this);
            }
        }
   
        // Run parent Start function
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Process Keyboard Inputs
        ProcessInputs();

        // Run parent Update function
        base.Update();
    }

    public void OnDestroy()
    {
        // If there is a game manager instance
        if (GameManager.instance != null)
        {
            // And if there is a list of player
            if (GameManager.instance.players != null)
            {
                // Remove this controller from the list
                GameManager.instance.players.Remove(this);
            }
        }
    }

    // Function to process the inputs from the keyborad
    public void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }

        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }

        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }

        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
        }

        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }

        if (Input.GetKeyUp(shootKey))
        {
            pawn.ResetNoise();
        }

        if (Input.GetKeyUp(moveForwardKey))
        {
            pawn.ResetNoise();
        }
        if (Input.GetKeyUp(moveBackwardKey))
        {
            pawn.ResetNoise();
        }
        if (Input.GetKeyUp(rotateClockwiseKey))
        {
            pawn.ResetNoise();
        }
        if (Input.GetKeyUp(rotateCounterClockwiseKey))
        {
            pawn.ResetNoise();
        }

    }


}
