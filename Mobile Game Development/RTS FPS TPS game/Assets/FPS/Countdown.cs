using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
    public GameObject lose;
    public GameObject Win;
    public Text time;
    private float timer;
    public float countdownTimer = 10f;
	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        lose.SetActive(false);
        Win.SetActive(false);
        timer = countdownTimer;        
	}
	
	// Update is called once per frame
	void Update () {
		if(timer >=0f)
        {
            timer -= Time.deltaTime;
            time.text = timer.ToString("F");
        }
        else if(timer <= 0f)
        {
            Time.timeScale = 0;
            timer = 0f;
            if(PlayerHealth.curHealth <=0)
            {
                lose.SetActive(true);
            }
            else
            {
                Win.SetActive(true);
            }
                  
        }
	}
}
