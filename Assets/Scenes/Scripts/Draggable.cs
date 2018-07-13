using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	private RectTransform rectTransform = null;
	
	public Transform parentToReturnTo = null;

	private GameObject placeholder = null;
	public Transform placeholderParent = null;
	
	void Awake()
	{
		rectTransform = GetComponent<RectTransform> ();
	}
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		placeholder = new GameObject();
		placeholder.transform.SetParent(this.transform.parent);
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;
		
		RectTransform rt = placeholder.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2 (300, 480);
		
		Debug.Log(le.preferredWidth);
		Debug.Log(le.preferredHeight);
		
		placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
		
		parentToReturnTo = this.transform.parent;
		placeholderParent = parentToReturnTo;
		this.transform.SetParent(this.transform.parent.parent);
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
		Debug.Log("OnBeginDrag");
	}

	public void OnDrag(PointerEventData eventData)
	{
		//Debug.Log(eventData);
//		this.transform.position = eventData.position;

		if (placeholder.transform.parent != placeholderParent)
		{
			placeholder.transform.SetParent(placeholderParent);
		}

		Vector3 result = Vector3.zero;

		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, Camera.main,
			out result))
		{
			rectTransform.position = result;
			//Debug.Log("OnDrag");
		}

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("OnEndDtag");
		this.transform.SetParent(parentToReturnTo);
		this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
		GetComponent<CanvasGroup> ().blocksRaycasts = true;

		Destroy(placeholder);
	}
	
}
