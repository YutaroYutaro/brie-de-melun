using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;

    public Transform ParentToReturnTo;

    private GameObject _placeholder;

    public Transform PlaceholderParent;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _placeholder = new GameObject();
        _placeholder.transform.SetParent(transform.parent);
        LayoutElement le = _placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;

        RectTransform rt = _placeholder.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(300, 480);


        _placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

        ParentToReturnTo = transform.parent;
        PlaceholderParent = ParentToReturnTo;
        transform.SetParent(transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_placeholder.transform.parent != PlaceholderParent)
        {
            _placeholder.transform.SetParent(PlaceholderParent);
        }

        Vector3 result;

        if (RectTransformUtility
            .ScreenPointToWorldPointInRectangle(
                _rectTransform,
                eventData.position,
                Camera.main,
                out result
            )
        )
        {
            _rectTransform.position = result;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(ParentToReturnTo);
        transform.SetSiblingIndex(_placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        Destroy(_placeholder);
    }
}