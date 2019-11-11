using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour {

    public static int livesValue = 3;
    public Text lives;

	// Update is called once per frame
	public void Update () {
        lives.text ="Lives : " + livesValue.ToString();
    }
}
