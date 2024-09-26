using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableController : MonoBehaviour
{
    public GameObject[] brokenPiece;
    public int maxPieces = 5;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && PlayerController.instance.isDashing())
        {
            Destroy(gameObject);
            int piecesToDrop = Random.Range(1, maxPieces);
            for (int i = 0; i < piecesToDrop; i++)
            {
                int selectedPiece = Random.Range(0, brokenPiece.Length);
                Instantiate(brokenPiece[selectedPiece], transform.position, transform.rotation);
            }
        }
    }
}
