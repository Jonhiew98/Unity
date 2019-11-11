using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FPSMoveController : MonoBehaviour,IDragHandler,IEndDragHandler{
    public Transform root;
    RectTransform rect;
    Vector3 origin;
    [SerializeField] Vector3 direction;

    float magnitude;
    public float sensitivity = 0.5f;

    // Use this for initialization
    void Start () {
        rect = this.GetComponent<RectTransform>();
        origin = rect.position;
	}

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
        direction = rect.position - origin;
        direction.Normalize();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rect.position = origin;
        direction = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        magnitude = Vector3.Distance(rect.position, origin);

        Vector3 velocity = Vector3.zero;
        velocity.x = direction.x * magnitude * sensitivity;
        velocity.z = direction.y * magnitude * sensitivity;

        root.Translate(velocity * Time.deltaTime, Space.Self);
	}
}
