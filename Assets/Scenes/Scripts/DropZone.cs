using UnityEngine.EventSystems;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {   
        if (eventData.pointerDrag == null)
            return;
        
        Debug.Log ("OnPointerEnter");
        
        Draggable dragObjectDraggable = eventData.pointerDrag.GetComponent<Draggable>();

        if (dragObjectDraggable != null)
        {
            dragObjectDraggable.placeholderParent = transform;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;
        
        Debug.Log ("OnPointerExit");
        
        Draggable dragObjectDraggable = eventData.pointerDrag.GetComponent<Draggable>();

        if (dragObjectDraggable != null && dragObjectDraggable.placeholderParent == transform)
        {
            dragObjectDraggable.placeholderParent = dragObjectDraggable.parentToReturnTo;
        }
    }
    
    public async void OnDrop(PointerEventData eventData)
    {
        Debug.Log (eventData.pointerDrag.name + "was dropped on " + gameObject.name);

        GameObject dragGameObject = eventData.pointerDrag;
        Draggable dragGameObjectDraggable = dragGameObject.GetComponent<Draggable>();

        if (dragGameObjectDraggable != null)
        {
            dragGameObjectDraggable.parentToReturnTo = transform;
            Debug.Log(dragGameObject.tag);
            await Task.Delay(TimeSpan.FromSeconds(1.0f));
            Destroy(dragGameObject);
        }
        
    }
}
