using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float timeBetweenShots;
    private float shotCooldown;

    public string gunName;
    public Sprite gunUI;

    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            handleShooting();
        }
    }

    void handleShooting()
    {
        if (shotCooldown > 0)
        {
            shotCooldown -= Time.deltaTime;
        }
        else
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                shotCooldown = timeBetweenShots;
                AudioManager.instance.PlaySFX(12);
            }
        }
    }
}
