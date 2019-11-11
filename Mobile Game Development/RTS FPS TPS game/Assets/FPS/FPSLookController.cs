using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FPSLookController : MonoBehaviour, IDragHandler, IEndDragHandler {
    public Transform root;
    public Transform yawRoot;

    RectTransform rect;
    Vector3 origin;
    [SerializeField] Vector3 direction;
    Vector3 angles;
    float distance;
    float magnitude;
    public float sensitivity = 0.5f;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        origin = rect.position; // Set the origin point
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

    void Update()
    {
        //Calculate sensitivity and magnitude here
        magnitude = Vector3.Distance(rect.position, origin);

        angles.y += direction.x * magnitude * sensitivity;                                        
        angles.x += direction.y * magnitude * sensitivity;

        angles.x = Mathf.Clamp(angles.x, -30f, 30f); // Clamp the values so you dont do a somersault

        root.rotation = Quaternion.Euler(0f, angles.y, 0f);
        yawRoot.rotation = Quaternion.Euler(-angles.x, angles.y, 0f);
    }
}
