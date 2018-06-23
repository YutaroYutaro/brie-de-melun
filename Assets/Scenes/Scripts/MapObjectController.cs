using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;


public class MapObjectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Color defaultColor;

    void Start()
    {
        defaultColor = GetComponent<Renderer>().material.color;

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Renderer>().material.color = defaultColor;
    }
}
