using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScores : MonoBehaviour {

    public static int scoresValue = 0;
    public Text scores;

	// Update is called once per frame
	void Update () {
        scores.text = "Scores : " + scoresValue.ToString();
    }
}
