using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;

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
            PlayerHealthController.instance.heal(healAmount);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(7);
        }
    }
}
