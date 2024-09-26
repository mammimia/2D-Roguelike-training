using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth = 100;

    public float damageInvincLength = 1f;
    private float invincCooldown;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
    }

    void Update()
    {
        if (invincCooldown > 0)
        {
            invincCooldown -= Time.deltaTime;

            if (invincCooldown <= 0)
            {
                Color playerBodyColor = PlayerController.instance.body.color;
                PlayerController.instance.body.color = new Color(playerBodyColor.r, playerBodyColor.b, playerBodyColor.g, 1f);
            }
        }
    }

    public void takeDamage(int damage)
    {
        if (invincCooldown <= 0)
        {

            currentHealth -= damage;
            invincCooldown = damageInvincLength;

            Color playerBodyColor = PlayerController.instance.body.color;
            PlayerController.instance.body.color = new Color(playerBodyColor.r, playerBodyColor.b, playerBodyColor.g, 0.5f);

            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
            }

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
        }
    }
}