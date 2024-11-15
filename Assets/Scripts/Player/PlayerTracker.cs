using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker instance;

    public int currentCoins, currentHealth, maxHealth;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
