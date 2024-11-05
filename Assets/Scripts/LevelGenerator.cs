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

    public Directon selectedDirection;

    public float xOffset = 18;
    public float yOffset = 10;

    // Start is called before the first frame update
    void Start()
    {
        // Create starting room
        Instantiate(layoutRoom, generatorPoint.position, generatorPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;

        selectedDirection = (Directon)Random.Range(0, 4);
        moveGenerationPoint();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moveGenerationPoint()
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
}
