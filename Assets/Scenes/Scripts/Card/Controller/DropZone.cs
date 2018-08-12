using UnityEngine.EventSystems;
using UnityEngine;
using System;
using System.Linq;
using System.Threading.Tasks;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameObject.Find("PhaseManager").GetComponent<PhaseManager>().GetNowPhase() == "SelectUseCard")
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
        if (GameObject.Find("PhaseManager").GetComponent<PhaseManager>().GetNowPhase() == "SelectUseCard")
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
        if (name == "Hand")
            return;

        if (GameObject.Find("PhaseManager").GetComponent<PhaseManager>().GetNowPhase() == "SelectUseCard")
        {
            Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);

            GameObject dragGameObject = eventData.pointerDrag;
            Draggable dragGameObjectDraggable = dragGameObject.GetComponent<Draggable>();

            if (dragGameObjectDraggable != null)
            {
                switch (dragGameObject.tag)
                {
                    case "MoveCard":
                        if (GameObject.Find("Player1Units").transform.childCount == 0)
                        {
                            dragGameObjectDraggable.placeholderParent = dragGameObjectDraggable.parentToReturnTo;
                            Debug.Log("Don't exist MyUnit.");
                            return;
                        }

                        Debug.Log("This card is a " + dragGameObject.tag);
                        GameObject.Find("PhaseManager").GetComponent<PhaseManager>().SetNextPhase("SelectMoveUnit");
                        Debug.Log("Phase: SelectMoveUnit");

                        break;

                    case "AttackCard":
                        Debug.Log("This card is a " + dragGameObject.tag);

                        UnitAttackManager unitAttackManager =
                            GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>();

                        //攻撃できるユニットが存在するか判定
                        if (!(unitAttackManager.ExistAttackTargetUnit()))
                        {
                            dragGameObjectDraggable.placeholderParent = dragGameObjectDraggable.parentToReturnTo;
                            Debug.Log("Don't exist Attacker or Target.");
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
                                GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                                    .SetNextPhase("SelectUseCard");
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
                            GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                                .SetNextPhase("SelectAttackerUnit");
                        }

                        break;

                    case "SummonCard":
                        if (GameObject.Find("Player1Units").transform.childCount != 0)
                        {
                            Transform player1UnitChildren = GameObject.Find("Player1Units").transform;
                            bool existUnit1 = false;
                            bool existUnit2 = false;
                            bool existUnit3 = false;

                            foreach (Transform player1UnitChild in player1UnitChildren)
                            {
                                if (Mathf.RoundToInt(player1UnitChild.position.x) == 1 &&
                                    Mathf.RoundToInt(player1UnitChild.position.z) == 0)
                                {
                                    Debug.Log("Exist Unit (1, 1, 0)");
                                    existUnit1 = true;
                                }
                                else if (Mathf.RoundToInt(player1UnitChild.position.x) == 2 &&
                                         Mathf.RoundToInt(player1UnitChild.position.z) == 0)
                                {
                                    Debug.Log("Exist Unit (2, 1, 0)");
                                    existUnit2 = true;
                                }
                                else if (Mathf.RoundToInt(player1UnitChild.position.x) == 3 &&
                                         Mathf.RoundToInt(player1UnitChild.position.z) == 0)
                                {
                                    Debug.Log("Exist Unit (3, 1, 0)");
                                    existUnit3 = true;
                                }

                                if (existUnit1 && existUnit2 && existUnit3)
                                {
                                    Debug.Log("Can't Summon");
                                    dragGameObjectDraggable.placeholderParent =
                                        dragGameObjectDraggable.parentToReturnTo;
                                    return;
                                }
                            }
                        }

                        GameObject.Find("UnitSummonGenerator").GetComponent<UnitSummonGenerator>().SummonUnitType =
                            dragGameObject.GetComponent<SummonUnitType>().SummonunitType;
                        Debug.Log("This is a SummonCard");
                        Debug.Log("Phase: SelectMiniMapPositionUnitSummon");
                        GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                            .SetNextPhase("SelectMiniMapPositionUnitSummon");

                        break;

                    case "ReconnaissanceCard":
                        int numberOfReconnaissanceUnit = 0;
                        GameObject aloneReconnaissanceUnit = new GameObject();
                        foreach (Transform unit in GameObject.Find("Player1Units").transform)
                        {
                            if (unit.gameObject.CompareTag("ReconnaissanceUnit"))
                            {
                                ++numberOfReconnaissanceUnit;
                                aloneReconnaissanceUnit = unit.gameObject;
                            }
                        }

                        if (numberOfReconnaissanceUnit == 1)
                        {
                            Debug.Log("This is a ReconnaissanceCard");
                            aloneReconnaissanceUnit.GetComponent<UnitReconnaissanceController>().UnitReconnaissance();

                            Debug.Log("Phase: SelectUseCard");
                            GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                                .SetNextPhase("SelectUseCard");
                        }
                        else if (numberOfReconnaissanceUnit > 1)
                        {
                            Debug.Log("Phase: SelectReconnaissanceUnit");
                            GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                                .SetNextPhase("SelectReconnaissanceUnit");
                        }
                        else
                        {
                            dragGameObjectDraggable.placeholderParent = dragGameObjectDraggable.parentToReturnTo;
                            Debug.Log("Don't exist ReconnaissanceUnit.");
                            return;
                        }

                        break;

                    default:
                        dragGameObjectDraggable.placeholderParent = dragGameObjectDraggable.parentToReturnTo;
                        Debug.Log("Don't exist this card type.");
                        return;
                }

                dragGameObjectDraggable.parentToReturnTo = transform;
                await Task.Delay(TimeSpan.FromSeconds(1.0f));
                dragGameObject.transform.SetParent(GameObject.Find("Graveyard").transform);
                dragGameObject.gameObject.SetActive(false);
            }
        }
    }
}