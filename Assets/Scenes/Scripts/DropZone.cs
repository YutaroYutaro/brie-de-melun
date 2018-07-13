using UnityEngine.EventSystems;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    async void Update()
    {
        if (this.CompareTag("UsedCardZone"))
        {
            foreach (Transform transform in gameObject.transform)
            {
                if (transform.CompareTag("MoveCard"))
                {
                    var go = transform.gameObject;
                    await Task.Delay(TimeSpan.FromSeconds(0.5f));
                    Destroy(go);
                }
            }
        }
    }

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
