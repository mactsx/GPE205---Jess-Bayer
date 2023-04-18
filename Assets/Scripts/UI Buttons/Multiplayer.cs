using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Multiplayer : MonoBehaviour
{
    public void SetMultiplayer(Toggle toggleVal)
    {
        GameManager.instance.setMultiplayer = toggleVal.isOn;
    }
}
