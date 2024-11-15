using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private bool canSelect;

    public GameObject message;

    public PlayerController playerToSpawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canSelect)
        {
            Vector3 playerPos = PlayerController.instance.transform.position;
            Destroy(PlayerController.instance.gameObject);
            PlayerController newPlayer = Instantiate(playerToSpawn, playerPos, playerToSpawn.transform.rotation);
            PlayerController.instance = newPlayer;
            gameObject.SetActive(false);

            CameraController.instance.target = newPlayer.transform;

            CharacterSelectManager.instance.activePlayer = newPlayer;
            CharacterSelectManager.instance.activeCharSelector.gameObject.SetActive(true);
            CharacterSelectManager.instance.activeCharSelector = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            message.SetActive(true);
            canSelect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            message.SetActive(false);
            canSelect = false;
        }
    }
}
