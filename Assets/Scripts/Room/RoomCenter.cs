using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openWhenEnemiesCleared;
    public List<GameObject> enemies = new List<GameObject>();

    public RoomController room;

    // Start is called before the first frame update
    void Start()
    {
        if (openWhenEnemiesCleared)
        {
            room.closeWhenEntered = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (room.isActive && enemies.Count > 0 && openWhenEnemiesCleared)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                room.openDoors();
            }
        }
    }
}
