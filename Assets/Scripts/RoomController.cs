using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [HideInInspector]
    public bool closeWhenEntered;

    public GameObject[] doors;

    [HideInInspector]
    public bool isActive;

    void Start()
    {

    }

    void Update()
    {

    }


    public void openDoors()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }

        closeWhenEntered = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.setTarget(transform);

            if (closeWhenEntered)
            {
                foreach (GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }

            isActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isActive = false;
        }
    }
}
