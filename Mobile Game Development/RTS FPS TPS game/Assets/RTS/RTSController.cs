using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSController : MonoBehaviour {
    [SerializeField] Rect rect;
    Vector2 startPoint;
    [SerializeField] Texture tex;
    bool isDrawing = false;
    bool canShow = false;

    RaycastHit hit;
    Ray ray;

    [SerializeField]List<GameObject> selectedUnits = new List<GameObject>();

    private bool isSelected = true;

    // Update is called once per frame
    void Update()
    {
        HighlightSelect();
    }

    void HighlightSelect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDrawing = true;
            startPoint = Input.mousePosition;         
        }

        if (Input.GetMouseButton(0))
        {
            rect = new Rect(
            startPoint.x,
            startPoint.y,
            Input.mousePosition.x - startPoint.x,
            Input.mousePosition.y - startPoint.y
            );
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrawing = false;
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Units");
            selectedUnits = new List<GameObject>();

            foreach (GameObject go in gos)
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(go.transform.position);

                if (rect.Contains(pos, true))
                {                    
                    selectedUnits.Add(go);
                    go.GetComponent<Renderer>().material.color = Color.cyan;
                }
                else
                {
                    go.GetComponent<Renderer>().material.color = Color.white;                  
                }
            }

            for (int i = 0; i < selectedUnits.Count; i++)
            {
                selectedUnits[i].GetComponent<AgentFollow>().moveSelected = true;
                UnitNumber.unitCount += selectedUnits.Count;
            }
        }
    }

    void OnGUI()
    {
        if(isDrawing)
        {
            Rect texRect = rect;
            texRect.Set(rect.x, Screen.height - rect.y, rect.width, -rect.height);
            GUI.DrawTexture(texRect, tex);
        }   
    }
}
