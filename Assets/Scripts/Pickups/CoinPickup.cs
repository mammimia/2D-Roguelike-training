using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    public int coinValue = 1;

    public float delayToBeCollected = 0.5f;

    private void Update()
    {
        if (delayToBeCollected > 0)
        {
            delayToBeCollected -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && delayToBeCollected <= 0)
        {
            LevelManager.instance.getCoins(coinValue);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(5);
        }
    }
}
