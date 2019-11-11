using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitNumber : MonoBehaviour {
    public Text unitIndicator;
    public static int unitCount;
    public int unitAmount = 7;
    // Use this for initialization
    void Start () {
        unitCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        unitIndicator.text = unitCount.ToString() + " / " + unitAmount.ToString();
    }
}
