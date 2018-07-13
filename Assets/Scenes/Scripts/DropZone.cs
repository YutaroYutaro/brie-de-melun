using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log ("OnPointerEnter");
        
        if (eventData.pointerDrag == null)
            return;
        
        Draggable dragObjectDraggable = eventData.pointerDrag.GetComponent<Draggable>();

        if (dragObjectDraggable != null)
        {
            dragObjectDraggable.placeholderParent = transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log ("OnPointerExit");
        
        if (eventData.pointerDrag == null)
            return;
        
        Draggable dragObjectDraggable = eventData.pointerDrag.GetComponent<Draggable>();

        if (dragObjectDraggable != null && dragObjectDraggable.placeholderParent == transform)
        {
            dragObjectDraggable.placeholderParent = dragObjectDraggable.parentToReturnTo;
        }
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log (eventData.pointerDrag.name + "was dropped on " + gameObject.name);
        
        Draggable dragObjectDraggable = eventData.pointerDrag.GetComponent<Draggable>();

        if (dragObjectDraggable != null)
        {
            dragObjectDraggable.parentToReturnTo = transform;
        }
    }
}
