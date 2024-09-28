using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableController : MonoBehaviour
{
    public GameObject[] brokenPiece;
    public int maxPieces = 5;

    public bool shouldDrop;
    public GameObject[] dropableItems;
    public float dropRate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerController.instance.isDashing())
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(0);
            handleBrokenPieces();
            handleItemDrop();
        }
    }

    private void handleBrokenPieces()
    {
        int piecesToDrop = Random.Range(1, maxPieces);
        for (int i = 0; i < piecesToDrop; i++)
        {
            int selectedPiece = Random.Range(0, brokenPiece.Length);
            Instantiate(brokenPiece[selectedPiece], transform.position, transform.rotation);
        }
    }

    private void handleItemDrop()
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
