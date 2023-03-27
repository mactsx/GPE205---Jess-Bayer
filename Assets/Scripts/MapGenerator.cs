using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public int rows;
    public int cols;
    private float roomWidth = 50.0f;
    private float roomHeight = 50.0f;
    private Room[,] mapGrid;
    public bool isMapOfTheDay;
    public int mapSeed;

    // Start is called before the first frame update
    void Start()
    {
        if (isMapOfTheDay)
        {
            mapSeed = DateToInt(DateTime.Now.Date);
        }
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Returns a random room tile
    public GameObject RandomRoomPrefab()
    {
        return roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Length)];
    }


    // Generate Map
    public void GenerateMap()
    {
        // Create the matrix and set how many rows and cols
        mapGrid = new Room[cols, rows];

        // Iterate through rows
        for (int r = 0; r < rows; r++)
        {
            // Iterate through each row
            for (int c = 0; c < cols; c++)
            {
                // Find the location
                float xPosition = roomWidth * c;
                float yPosition = roomHeight * r;
                Vector3 newPosition = new Vector3 (xPosition, 0.0f, yPosition);

                // Create a new tile at that location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                // Set its parent
                tempRoomObj.transform.parent = this.transform;

                // Give it a new name
                tempRoomObj.name = "Room_" + r + "," + c;

                // Get the room object
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                // Open doors - north and south
                // If on the bottom row, open the north door
                if (r == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                // Otherwise, if on the top row, open the south door
                else if (r == rows-1)
                {
                    Destroy(tempRoom.doorSouth);
                }
                // Otherwise, in the middle so open both doors
                else
                {
                    Destroy(tempRoom.doorNorth);
                    Destroy(tempRoom.doorSouth);
                }

                // Open doors - east or west
                // If in first colomn, open the east door
                if (c == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                // Otherwise, if in the last colomn, open the west door
                else if (c == cols-1)
                {
                    Destroy(tempRoom.doorWest);
                }
                // Otherwise, in the middle so open both doors
                else
                {
                    Destroy(tempRoom.doorEast);
                    Destroy(tempRoom.doorWest);
                }

                // Save it to the grid array
                mapGrid[c, r] = tempRoom;

                if (mapSeed > 0)
                {
                    // Set the seed based on the inputted number
                    UnityEngine.Random.InitState(mapSeed);
                }
                else
                {
                    // Set the seed based on the time
                    UnityEngine.Random.InitState(DateToInt(DateTime.Now));
                }

            }

        }

    }

    public int DateToInt(DateTime dateToUse)
    {
        // Add up the date and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }



}


