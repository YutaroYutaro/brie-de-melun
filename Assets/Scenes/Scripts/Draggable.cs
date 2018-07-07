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
		
		Vector2 result = Vector2.zero;

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, Camera.main,
			out result))
		{
//			Debug.Log("OnDrag");
//			Debug.Log(result.normalized);
			rectTransform.localPosition = result;
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
	
//	private Vector2 GetLocalPosition(Vector2 screenPosition)
//	{
//    
//		RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPosition, Camera.main, out result);
//       
//		return result;
//	}
//	
}
