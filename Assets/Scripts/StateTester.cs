using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTester : MonoBehaviour
{ 
    public KeyCode title;
    public KeyCode main;
    public KeyCode options;
    public KeyCode credits;
    public KeyCode gameover;
    public KeyCode gameplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(title))
        {
            Debug.Log("Title Screen");
            GameManager.instance.ActivateTitleScreen();
        }
        if (Input.GetKey(main)) 
        {
            Debug.Log("Main Menu");
            GameManager.instance.ActivateMainMenuState();
        }
        if (Input.GetKey(options))
        {
            Debug.Log("Options");
            GameManager.instance.ActivateOptionsMenuState();
        }
        if (Input.GetKey(credits))
        {
            Debug.Log("Credits Page");
            GameManager.instance.ActivateCreditsState();
        }
        if (Input.GetKeyDown(gameplay))
        {
            Debug.Log("PlayingGame");
            GameManager.instance.ActivateGameplayState();
        }
        if (Input.GetKey(gameover))
        {
            Debug.Log("Game Over");
            GameManager.instance.ActivateGameOverState();
        }

    }
}
