using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Create a singleton of a gameManager
    public static GameManager instance;
    // Prefabs
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    // Create spawn point for the player tank
    public Transform playerSpawnTransform;
    // Create a list of player
    public List<PlayerController> players;


    // Run before start
    private void Awake()
    {
        // Check if the instance is already created
        if (instance == null)
        {
            // If there is no instance, set the instance to this one
            instance = this;
            // Dont destroy the instance if a new scene is loaded
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If the else runs, there is already a game instance, so destroy it
            Destroy(gameObject);
        }
    
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();   
    }


    // Function to spawn the player
    public void SpawnPlayer()
    {
        // Spawn controller at origin
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the tank pawn and connect it to the controller
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation) as GameObject;

        // Get the components
        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }

}
