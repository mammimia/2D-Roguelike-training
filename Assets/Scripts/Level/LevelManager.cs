using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 4f;

    public string nextLevel;

    public int currentCoins;

    public bool isPaused;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIController.instance.coinText.text = currentCoins.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseUnpause();
        }
    }

    public IEnumerator endLevel()
    {
        AudioManager.instance.playWinMusic();
        PlayerController.instance.canMove = false;
        UIController.instance.startFadeToBlack();
        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(nextLevel);
    }

    public void pauseUnpause()
    {
        if (UIController.instance.pauseMenu.activeInHierarchy)
        {
            UIController.instance.pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.instance.canMove = true;
            isPaused = false;
        }
        else
        {
            UIController.instance.pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            PlayerController.instance.canMove = false;
            isPaused = true;
        }
    }

    public void getCoins(int amount)
    {
        currentCoins += amount;
        UIController.instance.coinText.text = currentCoins.ToString();
    }

    public void spendCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
        }

        UIController.instance.coinText.text = currentCoins.ToString();
    }
}
