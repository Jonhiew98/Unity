using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    [SerializeField] List<GameObject> selectedUnits = new List<GameObject>();
    public static bool win = false;

    private void OnTriggerEnter(Collider other)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Units");
        selectedUnits.Add(other.gameObject);
        foreach (GameObject go in gos)
        {            
            if(selectedUnits.Count == 7)
            {
                win = true;
                Debug.Log("Done");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        selectedUnits.Clear();
    }
}
