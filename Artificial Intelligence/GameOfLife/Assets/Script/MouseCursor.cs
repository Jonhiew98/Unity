using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour {
    Vector2 mousePos;
    Vector2 cursorPos;

	// Update is called once per frame
	void Update () {
        mousePos = Input.mousePosition;
        cursorPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = cursorPos;
    }
}
