using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;

    public int distanceToEnd;

    public Color startColor, endColor;

    public Transform generatorPoint;

    public enum Directon
    {
        UP,
        RIGHT,
        DOWN,
        LEFT
    }

    private Directon selectedDirection;

    public float xOffset = 18;
    public float yOffset = 10;

    public LayerMask roomLayer;

    // Start is called before the first frame update
    void Start()
    {
        // Create starting room
        GameObject startRoom = createRoom();
        startRoom.GetComponent<SpriteRenderer>().color = startColor;

        for (int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = createRoom();

            if (i == distanceToEnd - 1)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
            }
        }
    }

    private GameObject createRoom()
    {
        GameObject newRoom = Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);

        selectDirection();
        moveGenerationPoint();

        // If there is a room on new generation point
        while (Physics2D.OverlapCircle(generatorPoint.position, .2f, roomLayer))
        {
            // Move to same direction to break the infinite loop
            // In case there is 4 room arround a room
            moveGenerationPoint();
        }

        return newRoom;
    }

    private void moveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case Directon.UP:
                generatorPoint.position += new Vector3(0, yOffset, 0);
                break;
            case Directon.DOWN:
                generatorPoint.position -= new Vector3(0, yOffset, 0);
                break;
            case Directon.RIGHT:
                generatorPoint.position += new Vector3(xOffset, 0, 0);
                break;
            case Directon.LEFT:
                generatorPoint.position -= new Vector3(xOffset, 0, 0);
                break;
        }
    }

    private void selectDirection()
    {
        selectedDirection = (Directon)Random.Range(0, 4);
    }
}
