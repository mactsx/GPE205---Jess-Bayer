using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainButton : MonoBehaviour
{
    public void ChangeToMainMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateMainMenuState();
        }
    }
}
