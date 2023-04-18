using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameButton : MonoBehaviour
{
    public void ChangeToPlayGame()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateGameplayState();
        }
    }
}
