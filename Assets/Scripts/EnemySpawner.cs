using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //public AIController AIControllerPrefab;
    public GameObject[] enemyTypes;
    public GameObject AIControllerPrefab;
    private GameObject AIPawnPrefab;
    // Create spawn point for the player tank
    public Transform AISpawnTransform;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Returns a random enemy
    public void SetRandomEnemyPrefab()
    {
        AIPawnPrefab = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Length)] as GameObject;
    }

    public void SpawnEnemy()
    {
        SetRandomEnemyPrefab();

        // Spawn controller at origin
        GameObject newAIObj = Instantiate(AIControllerPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        // Spawn the tank pawn and connect it to the controller
        GameObject newAIPawnObj = Instantiate(AIPawnPrefab, AISpawnTransform.position, AISpawnTransform.rotation) as GameObject;

        // Get the components
        Controller newController = newAIObj.GetComponent<Controller>();
        Pawn newPawn = newAIPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }
}
