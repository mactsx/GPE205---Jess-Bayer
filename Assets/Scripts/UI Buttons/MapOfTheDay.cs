using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapOfTheDay : MonoBehaviour
{
    public MapGenerator mapGen;

    public void UseMapOfTheDay(Toggle toggleVal)
    {
        mapGen.isMapOfTheDay = toggleVal.isOn;
        
    }
}
