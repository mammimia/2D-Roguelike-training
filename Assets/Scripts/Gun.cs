using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float timeBetweenShots;
    private float shotCooldown;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            handleShooting();

        }
    }

    void handleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            shotCooldown = timeBetweenShots;
            AudioManager.instance.PlaySFX(12);
        }

        if (Input.GetMouseButton(0))
        {
            shotCooldown -= Time.deltaTime;

            if (shotCooldown <= 0)
            {
                shotCooldown = timeBetweenShots;
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                AudioManager.instance.PlaySFX(12);
            }
        }
    }

}
