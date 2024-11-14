using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public Gun gunToEquip;
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
        Debug.Log("Triggered");
        if (other.tag == "Player" && delayToBeCollected <= 0)
        {
            Debug.Log("Player has picked up a gun");
            bool playerHasGun = hasGun();

            if (!playerHasGun)
            {
                Gun gunClone = Instantiate(gunToEquip);
                gunClone.transform.parent = PlayerController.instance.gunArm;
                gunClone.transform.position = PlayerController.instance.gunArm.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = new Vector3(1, 1, 1);
                PlayerController.instance.availableGuns.Add(gunClone);
                PlayerController.instance.currentGunIndex = PlayerController.instance.availableGuns.Count - 1;
                PlayerController.instance.switchGun();

                Destroy(gameObject);
                AudioManager.instance.PlaySFX(6);
            }
        }
    }

    private bool hasGun()
    {
        foreach (Gun gun in PlayerController.instance.availableGuns)
        {
            if (gun.gunName == gunToEquip.gunName)
            {
                return true;
            }
        }
        return false;
    }
}
