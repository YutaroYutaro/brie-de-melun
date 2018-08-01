using UnityEngine.EventSystems;
using UnityEngine;
using System;
using System.Linq;
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
            Debug.Log(eventData.pointerDrag.name + "was dropped on " + gameObject.name);

            GameObject dragGameObject = eventData.pointerDrag;
            Draggable dragGameObjectDraggable = dragGameObject.GetComponent<Draggable>();

            if (dragGameObjectDraggable != null)
            {
                if (dragGameObject.CompareTag("MoveCard"))
                {
                    Debug.Log("This card is a " + dragGameObject.tag);
                    GameObject.Find("PhaseManager").GetComponent<PhaseManager>().SetNextPhase("SelectMoveUnit");
                    Debug.Log("Phase: SelectMoveUnit");
                }
                else if (dragGameObject.CompareTag("AttackCard"))
                {
                    Debug.Log("This card is a " + dragGameObject.tag);

                    UnitAttackManager unitAttackManager =
                        GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>();

                    //攻撃できるユニットが存在するか判定
                    if (!(unitAttackManager.ExistAttackTargetUnit()))
                    {
                        dragGameObjectDraggable.placeholderParent = dragGameObjectDraggable.parentToReturnTo;
                        Debug.Log("Don't exist target.");
                        return;
                    }

                    //アタッカーユニットが1体か判定
                    if (unitAttackManager.GetAttackerAndTargetList().Count == 1)
                    {
                        //ターゲットユニットが1体か判定
                        if (unitAttackManager
                                .GetAttackerAndTargetList().First().Target.Count == 1)
                        {
                            unitAttackManager.SetSelectedAttackerAndTargetUnit(unitAttackManager
                                .GetAttackerAndTargetList().First());
                            unitAttackManager.MiniMapUnitAttack(unitAttackManager
                                .GetAttackerAndTargetList().First().Target.First());
                            GameObject.Find("PhaseManager").GetComponent<PhaseManager>().SetNextPhase("SelectUseCard");
                            Debug.Log("Phase: SelectUseCard");
                        }
                        else
                        {
                            unitAttackManager.SetSelectedAttackerAndTargetUnit(unitAttackManager
                                .GetAttackerAndTargetList().First());
                            Debug.Log("Phase: SelectAttackTargetUnit");
                            GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                                .SetNextPhase("SelectAttackTargetUnit");
                        }
                    }
                    else
                    {
                        Debug.Log("Phase: SelectAttackerUnit");
                        GameObject.Find("PhaseManager").GetComponent<PhaseManager>().SetNextPhase("SelectAttackerUnit");
                    }
                }

                dragGameObjectDraggable.parentToReturnTo = transform;
                await Task.Delay(TimeSpan.FromSeconds(1.0f));
                Destroy(dragGameObject);
            }
        }
    }
}