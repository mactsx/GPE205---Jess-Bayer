using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public int maxLives;
    public int currentLives;
    public GameObject targetTank;
    private Health targetHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentLives = maxLives; 
        targetHealth = targetTank.GetComponent<Health>();
    }

    void Update()
    {
        if (targetHealth.currentHealth <= 0)
        {
            //LoseLife();
        }
    }

    public void LoseLife()
    {
        currentLives--;
        GameManager.instance.ActivateGameOverState();

        if (currentLives <= 0)
        {
            GameManager.instance.ActivateMainMenuState();
        }
    }

}
