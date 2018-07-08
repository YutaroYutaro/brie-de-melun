using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	
	RectTransform rectTransform = null;
	
	
	void Awake()
	{
		rectTransform = GetComponent<RectTransform> ();
	}

	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log(eventData);
		
		Vector3 result = Vector3.zero;

		if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, Camera.main,
			out result))
		{
			rectTransform.position = result;
		}
		
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("OnBiginDrag");
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("OnEndDtag");
	} 
	
}
