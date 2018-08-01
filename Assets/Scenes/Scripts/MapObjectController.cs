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
        //現在の色を保存
        defaultColor = GetComponent<Renderer>().material.color;

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        //オブジェクトが回転しないように設定
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    //マップオブジェクトにマウスがホバーしたときに色を変更
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    //ホバーが解除されたら元の色に戻す
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Renderer>().material.color = defaultColor;
    }
}