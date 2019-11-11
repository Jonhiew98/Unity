using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour {
    public GameObject tutorial;
	// Use this for initialization
	void Start () {
        tutorial.SetActive(true);       
    }

    private void Update()
    {
        if (tutorial.activeSelf)
        {
            Time.timeScale = 0;
        }
    }


    public void OnClick()
    {
        Time.timeScale = 1;
        tutorial.SetActive(false);
    }
}
