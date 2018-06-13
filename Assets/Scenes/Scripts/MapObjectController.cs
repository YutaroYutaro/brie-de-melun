using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;


public class MapObjectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    //Material thisMaterial;
    Color defaultColor;
    //Renderer rend;

    void Start()
    {
        //thisMaterial = GetComponent<Renderer>().material;
        defaultColor = GetComponent<Renderer>().material.color;

        Debug.Log(defaultColor);

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        //rend = GetComponent<Renderer>();
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
