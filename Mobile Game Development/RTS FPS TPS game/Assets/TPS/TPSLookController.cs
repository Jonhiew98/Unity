using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TPSLookController : MonoBehaviour,IDragHandler, IEndDragHandler
{
    public Transform root;

    RectTransform rect;
    Vector3 origin;
    [SerializeField] Vector3 direction;
    float angles;

    // Use this for initialization
    void Start () {
        rect = GetComponent<RectTransform>();
        origin = rect.position; // Set the origin point
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position = eventData.position;
        direction = rect.position - origin;
        direction.Normalize();
        angles = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rect.position = origin;
        direction = Vector3.zero;
    }

    // Update is called once per frame
    void Update () {

        root.rotation = Quaternion.Lerp(root.rotation, Quaternion.Euler(0, angles, 0),Time.deltaTime* 60f);

    }
}
