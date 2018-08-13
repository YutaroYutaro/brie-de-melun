using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;


public class MapObjectController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Color _defaultColor;

    void Start()
    {
        //現在の色を保存
        _defaultColor = GetComponent<Renderer>().material.color;

        //オブジェクトが回転しないように設定
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    //マップオブジェクトにマウスがホバーしたときに色を変更
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    //ホバーが解除されたら元の色に戻す
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Renderer>().material.color = _defaultColor;
    }
}