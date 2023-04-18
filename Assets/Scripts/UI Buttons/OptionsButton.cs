using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    public void ChangeToOptionsMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateOptionsMenuState();
        }
    }
}
