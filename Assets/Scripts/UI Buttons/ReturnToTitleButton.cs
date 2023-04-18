using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToTitleButton : MonoBehaviour
{
    public void ChangeToTitleScreen()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateTitleScreen();
        }
    }
}
