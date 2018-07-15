using UnityEngine.EventSystems;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private string _nowPhase = null;

    public void OnPointerEnter(PointerEventData eventData)
    {   
        _nowPhase = GameObject.Find("PhaseManager").GetComponent<PhaseManager>().GetNowPhase();

        if (_nowPhase == "SelectUseCard")
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _nowPhase = GameObject.Find("PhaseManager").GetComponent<PhaseManager>().GetNowPhase();
        
        if (_nowPhase == "SelectUseCard")
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
    }
    
    public async void OnDrop(PointerEventData eventData)
    {
        _nowPhase = GameObject.Find("PhaseManager").GetComponent<PhaseManager>().GetNowPhase();
        
        if (_nowPhase == "SelectUseCard")
        {
            Debug.Log (eventData.pointerDrag.name + "was dropped on " + gameObject.name);

            GameObject dragGameObject = eventData.pointerDrag;
            Draggable dragGameObjectDraggable = dragGameObject.GetComponent<Draggable>();

            if (dragGameObjectDraggable != null)
            {
//                dragGameObjectDraggable.parentToReturnTo = transform;

                if (dragGameObject.CompareTag("MoveCard"))
                {
                    Debug.Log("This card is a " + dragGameObject.tag);
                    GameObject.Find("PhaseManager").GetComponent<PhaseManager>().SetNextPhase("SelectMoveUnit");
                }
                else if (dragGameObject.CompareTag("AttackCard"))
                {
                    Debug.Log("This card is a " + dragGameObject.tag);
                    if (!(GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>().ExistAttackTargetUnit()))
                    {
                        dragGameObjectDraggable.placeholderParent = dragGameObjectDraggable.parentToReturnTo;
                        Debug.Log("Don't exist target.");
                        return;
                    }
                }
            
                dragGameObjectDraggable.parentToReturnTo = transform;
                await Task.Delay(TimeSpan.FromSeconds(1.0f));
                Destroy(dragGameObject);
            }
        }
    }
}
