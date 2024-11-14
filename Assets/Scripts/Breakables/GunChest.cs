using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : MonoBehaviour
{
    public GunPickup[] possibleGuns;
    public SpriteRenderer theSR;
    public Sprite chestOpen;
    public bool isOpen;
    public GunPickup currentGun;
    public Transform gunSpawnPoint;

    public GameObject notification;
    private bool canOpen;

    // Start is called before the first frame update
    void Start()
    {
        int gunSelect = Random.Range(0, possibleGuns.Length);
        currentGun = possibleGuns[gunSelect];
    }

    // Update is called once per frame
    void Update()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }

        if (isOpen)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one, Time.deltaTime * 2f);
        }
    }

    public void OpenChest()
    {
        if (!isOpen)
        {
            isOpen = true;
            theSR.sprite = chestOpen;
            Instantiate(currentGun, gunSpawnPoint.position, gunSpawnPoint.rotation);
            theSR.sprite = chestOpen;
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !isOpen)
        {
            notification.SetActive(true);
            canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            notification.SetActive(false);
            canOpen = false;
        }
    }
}
