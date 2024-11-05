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

    // Start is called before the first frame update
    void Start()
    {
        // Create starting room
        GameObject startRoom = createRoom();
        startRoom.GetComponent<SpriteRenderer>().color = startColor;
        moveGenerationPoint();

        for (int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = createRoom();
            moveGenerationPoint();

            if (i == distanceToEnd - 1)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
            }
        }
    }

    private GameObject createRoom()
    {
        return Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation);
    }

    private void moveGenerationPoint()
    {
        selectedDirection = (Directon)Random.Range(0, 4);
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
}
