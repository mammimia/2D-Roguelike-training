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

    private GameObject endRoom;
    private List<GameObject> roomLayoutList = new List<GameObject>();
    private List<GameObject> generatedRoomOutlines = new List<GameObject>();

    public RoomPrefabs roomLayouts;

    public RoomCenter startRoomCenter, endRoomCenter;
    public List<RoomCenter> potentialRoomCenters;
    private Dictionary<int, GameObject> layoutMap;


    void Awake()
    {
        // Dictionary to map each layout key to the corresponding room layout prefab
        layoutMap = new Dictionary<int, GameObject>
        {
        { 1, roomLayouts.singleUp },
        { 2, roomLayouts.singleRight },
        { 3, roomLayouts.doubleUpRight },
        { 4, roomLayouts.singleDown },
        { 5, roomLayouts.doubleUpDown },
        { 6, roomLayouts.doubleRightDown },
        { 7, roomLayouts.tripleUpRightDown },
        { 8, roomLayouts.singleLeft },
        { 9, roomLayouts.doubleUpLeft },
        { 10, roomLayouts.doubleRightLeft },
        { 11, roomLayouts.tripleUpRightLeft },
        { 12, roomLayouts.doubleDownLeft },
        { 13, roomLayouts.tripleUpDownLeft },
        { 14, roomLayouts.tripleRightDownLeft },
        { 15, roomLayouts.fourway },
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        // Create starting room
        GameObject startRoom = createRoom();
        startRoom.GetComponent<SpriteRenderer>().color = startColor;

        // Create rest of the rooms
        for (int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = createRoom();

            if (i == distanceToEnd - 1)
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                endRoom = newRoom;
            }
            else
            {
                roomLayoutList.Add(newRoom);
            }
        }

        // Create room outlines
        createRoomOutline(Vector3.zero);

        foreach (GameObject room in roomLayoutList)
        {
            createRoomOutline(room.transform.position);
        }

        createRoomOutline(endRoom.transform.position);

        foreach (GameObject outline in generatedRoomOutlines)
        {
            bool shouldGenerateCenter = true;

            if (outline.transform.position == Vector3.zero)
            {
                Instantiate(startRoomCenter, outline.transform.position, Quaternion.identity);
                shouldGenerateCenter = false;
            }

            if (outline.transform.position == endRoom.transform.position)
            {
                Instantiate(endRoomCenter, outline.transform.position, Quaternion.identity);
                shouldGenerateCenter = false;
            }

            if (shouldGenerateCenter)
            {
                int randomCenter = Random.Range(0, potentialRoomCenters.Count);

                RoomCenter roomCenter = Instantiate(potentialRoomCenters[randomCenter], outline.transform.position, Quaternion.identity);
                roomCenter.room = outline.GetComponent<RoomController>();
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

    private void createRoomOutline(Vector3 roomPosition)
    {
        // Use bitwise flags to represent room connections.
        // 1 - Up, 2 - Right, 4 - Down, 8 - Left
        int layoutKey = 0;
        float overlapRadius = 2f;

        if (Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffset, 0f), overlapRadius))
            layoutKey |= 1; // Up
        if (Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0f, 0f), overlapRadius))
            layoutKey |= 2; // Right
        if (Physics2D.OverlapCircle(roomPosition - new Vector3(0f, yOffset, 0f), overlapRadius))
            layoutKey |= 4; // Down
        if (Physics2D.OverlapCircle(roomPosition - new Vector3(xOffset, 0f, 0f), overlapRadius))
            layoutKey |= 8; // Left

        Debug.Log(layoutKey);
        Debug.Log(layoutMap.ContainsKey(layoutKey));
        Debug.Log(layoutMap[layoutKey]);
        Debug.Log(roomPosition);

        // Instantiate the room layout prefab corresponding to the layout key
        if (layoutMap.TryGetValue(layoutKey, out GameObject layoutPrefab))
        {
            GameObject roomOutline = Instantiate(layoutPrefab, roomPosition, Quaternion.identity);
            generatedRoomOutlines.Add(roomOutline);
        }
    }
}

[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleUp, singleDown, singleRight, singleLeft,
    doubleUpDown, doubleRightLeft, doubleUpRight, doubleRightDown, doubleDownLeft, doubleUpLeft,
    tripleUpRightDown, tripleRightDownLeft, tripleUpDownLeft, tripleUpRightLeft,
    fourway;
}