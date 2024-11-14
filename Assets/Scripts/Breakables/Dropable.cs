using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dropable : MonoBehaviour
{
    public bool shouldDrop;
    public GameObject[] dropableItems;
    public float dropRate;

    public void handleItemDrop()
    {
        if (shouldDrop)
        {
            float dropChance = Random.Range(0f, 100f);
            if (dropChance < dropRate)
            {
                int itemToDrop = Random.Range(0, dropableItems.Length);
                Instantiate(dropableItems[itemToDrop], transform.position, transform.rotation);
            }
        }
    }
}
