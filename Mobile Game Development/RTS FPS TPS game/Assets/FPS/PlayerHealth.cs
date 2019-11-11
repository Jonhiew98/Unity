using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100f;
    public static float curHealth;
    public GameObject lose;
    public Text health;

    // Use this for initialization
    void Start()
    {
        lose.SetActive(false);
        curHealth = playerHealth;
        Time.timeScale = 1;
    }

    private void Update()
    {
        health.text = "Health: " + PlayerHealth.curHealth.ToString();
        if (curHealth <= 0)
        {
            Time.timeScale = 0;
            lose.SetActive(true);
        }
    }
}
