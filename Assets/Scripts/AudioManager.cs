using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource levelMusic, gameOverMusic, winMusic;

    void Awake()
    {
        instance = this;
    }

    public void playLevelMusic()
    {
        levelMusic.Play();
        gameOverMusic.Stop();
        winMusic.Stop();
    }

    public void playGameOverMusic()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
        winMusic.Stop();
    }

    public void playWinMusic()
    {
        levelMusic.Stop();
        gameOverMusic.Stop();
        winMusic.Play();
    }
}
