using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public GameObject purchaseMessage;
    public int itemCost;

    private bool inBuyZone;

    public enum ItemType { HealthRestore, HealthUpgrade, Weapon }
    public ItemType itemType;

    private void Update()
    {
        if (inBuyZone && Input.GetKeyDown(KeyCode.E))
        {
            TryPurchaseItem();
        }
    }

    // Method to handle the item purchase
    private void TryPurchaseItem()
    {
        // Ensure LevelManager and PlayerHealthController instances are not null
        if (LevelManager.instance == null || PlayerHealthController.instance == null)
        {
            Debug.LogWarning("LevelManager or PlayerHealthController instance is missing.");
            return;
        }

        // Check if player has enough coins
        if (LevelManager.instance.currentCoins >= itemCost)
        {
            LevelManager.instance.spendCoins(itemCost);

            switch (itemType)
            {
                case ItemType.HealthRestore:
                    RestorePlayerHealth();
                    break;

                case ItemType.HealthUpgrade:
                    UpgradePlayerHealth();
                    break;

                case ItemType.Weapon:
                    GrantPlayerWeapon();
                    break;
            }
        }
        else
        {
            Debug.Log("Not enough coins to purchase item.");
        }
    }

    // Specific methods to handle different item effects
    private void RestorePlayerHealth()
    {
        PlayerHealthController.instance.heal(PlayerHealthController.instance.maxHealth);
    }

    private void UpgradePlayerHealth()
    {
        // Implement max health logic here
        Debug.Log("Max health upgraded.");
    }

    private void GrantPlayerWeapon()
    {
        // Implement weapon-granting logic here
        Debug.Log("Weapon granted to the player.");
    }

    // Show purchase message on entering buy zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            purchaseMessage?.SetActive(true);
            inBuyZone = true;
        }
    }

    // Hide purchase message on exiting buy zone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            purchaseMessage?.SetActive(false);
            inBuyZone = false;
        }
    }
}
