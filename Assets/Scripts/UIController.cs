using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public Text healthText;

    public GameObject deathScreen;

    public Image fadeSceen;
    public float fadeSpeed = 2.5f;
    private bool fadeToBlack = false, fadeOutBlack = true;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }

    void Update()
    {
        handleFading();
    }

    public void handleFading()
    {
        if (fadeOutBlack)
        {
            fadeSceen.color = new Color(fadeSceen.color.r, fadeSceen.color.g, fadeSceen.color.b, Mathf.MoveTowards(fadeSceen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeSceen.color.a == 0f)
            {
                fadeOutBlack = false;
            }
        }

        if (fadeToBlack)
        {
            fadeSceen.color = new Color(fadeSceen.color.r, fadeSceen.color.g, fadeSceen.color.b, Mathf.MoveTowards(fadeSceen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeSceen.color.a == 1f)
            {
                fadeToBlack = false;
            }
        }
    }

    public void startFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }
}
