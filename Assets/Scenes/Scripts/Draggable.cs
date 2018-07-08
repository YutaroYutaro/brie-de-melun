using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	RectTransform rectTransform = null;
	
	private Transform parentToReturnTo = null;
	
	void Awake()
	{
		rectTransform = GetComponent<RectTransform> ();
	}
	
	public void OnBeginDrag(PointerEventData eventData)
	{
		parentToReturnTo = this.transform.parent;
		this.transform.SetParent(this.transform.parent.parent);
		Debug.Log("OnBeginDrag");
	}

	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log(eventData);
//		this.transform.position = eventData.position;

		Vector3 result = Vector3.zero;

		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, Camera.main,
			out result))
		{
			rectTransform.position = result;
			Debug.Log("OnDrag");
		}

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("OnEndDtag");
		this.transform.SetParent(parentToReturnTo);
	}
	
}
