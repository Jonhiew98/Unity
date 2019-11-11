using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starMovement : MonoBehaviour {

    public static float speed;
    float randX;

	// Update is called once per frame
	void Update () {
        StarMove();
	}

    void StarMove()
    {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        randX = Random.Range(min.x, max.x);

        if (transform.position.y < min.y)
        {
            transform.position = new Vector2(randX, max.y);
        }
    }
}
