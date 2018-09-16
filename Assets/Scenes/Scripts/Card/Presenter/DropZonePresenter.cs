using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;
using System.Linq;
using System.Threading.Tasks;
using Asset.Scripts.Cards;

public class DropZonePresenter : MonoBehaviour
{
    private bool _isSetBool;

    // Use this for initialization
    void Start()
    {
        ObservableEventTrigger observableEventTrigger = gameObject.AddComponent<ObservableEventTrigger>();

        observableEventTrigger.OnPointerEnterAsObservable()
            .Where(eventData =>
                PhaseManager.Instance.PhaseReactiveProperty.Value == "SelectUseCard"
                && eventData.pointerDrag != null
                && eventData.pointerDrag.GetComponent<Draggable>() != null
            )
            .Subscribe(eventData =>
                eventData.pointerDrag.GetComponent<Draggable>().PlaceholderParent = transform
            );

        observableEventTrigger.OnPointerExitAsObservable()
            .Where(eventData =>
                PhaseManager.Instance.GetNowPhase() == "SelectUseCard"
                && eventData.pointerDrag != null
                && eventData.pointerDrag.GetComponent<Draggable>() != null
                && eventData.pointerDrag.GetComponent<Draggable>() == transform
            )
            .Subscribe(eventData =>
                eventData.pointerDrag.GetComponent<Draggable>().PlaceholderParent =
                    eventData.pointerDrag.GetComponent<Draggable>().ParentToReturnTo
            );

        observableEventTrigger.OnDropAsObservable()
            .Where(eventData =>
                PhaseManager.Instance.GetNowPhase() == "SelectUseCard"
                && name != "Hand"
                && eventData.pointerDrag.GetComponent<Draggable>() != null
                && ManaModel.Instance.ManaReactiveProperty.Value >=
                eventData.pointerDrag.GetComponent<ManaOfCard>().Mana
            )
            .Subscribe(async eventData =>
                {
                    ManaModel.Instance.ManaReactiveProperty.Value -=
                        eventData.pointerDrag.GetComponent<ManaOfCard>().Mana;

                    GameObject dragGameObject = eventData.pointerDrag;
                    Draggable dragGameObjectDraggable = dragGameObject.GetComponent<Draggable>();

                    switch (dragGameObject.tag)
                    {
                        case "MoveCard":
                            MoveCardController moveCardController = new MoveCardController();
                            _isSetBool = moveCardController.SetCardTypePhase();

                            if (_isSetBool == false)
                            {
                                dragGameObjectDraggable.PlaceholderParent =
                                    dragGameObjectDraggable.ParentToReturnTo;

                                Debug.Log("Don't exist MyUnit.");
                                return;
                            }
                            break;

                        case "AttackCard":
                            Debug.Log("This card is a " + dragGameObject.tag);

                            UnitAttackManager unitAttackManager =
                                GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>();

                            //攻撃できるユニットが存在するか判定
                            if (!(unitAttackManager.ExistAttackTargetUnit()))
                            {
                                dragGameObjectDraggable.PlaceholderParent =
                                    dragGameObjectDraggable.ParentToReturnTo;

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
                                    unitAttackManager.SelectedAttacker =
                                        unitAttackManager.GetAttackerAndTargetList().First().Attacker;

                                    unitAttackManager.MiniMapUnitAttack(
                                        unitAttackManager.GetAttackerAndTargetList().First().Target.First()
                                    );

                                    GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                                        .SetNextPhase("SelectUseCard");

                                    Debug.Log("Phase: SelectUseCard");
                                }
                                else
                                {
                                    unitAttackManager.SelectedAttacker =
                                        unitAttackManager.GetAttackerAndTargetList().First().Attacker;

                                    GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                                        .SetNextPhase("SelectAttackTargetUnit");

                                    Debug.Log("Phase: SelectAttackTargetUnit");
                                }
                            }
                            else
                            {
                                GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                                    .SetNextPhase("SelectAttackerUnit");

                                Debug.Log("Phase: SelectAttackerUnit");
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

                                        dragGameObjectDraggable.PlaceholderParent =
                                            dragGameObjectDraggable.ParentToReturnTo;

                                        return;
                                    }
                                }
                            }

                            GameObject.Find("UnitSummonGenerator").GetComponent<UnitSummonGenerator>().SummonUnitType =
                                dragGameObject.GetComponent<SummonUnitType>().SummonunitType;

                            Debug.Log("This is a SummonCard");

                            GameObject.Find("PhaseManager")
                                .GetComponent<PhaseManager>()
                                .SetNextPhase("SelectMiniMapPositionUnitSummon");

                            Debug.Log("Phase: SelectMiniMapPositionUnitSummon");

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

                                aloneReconnaissanceUnit
                                    .GetComponent<UnitReconnaissanceController>()
                                    .UnitReconnaissance();

                                GameObject.Find("PhaseManager")
                                    .GetComponent<PhaseManager>()
                                    .SetNextPhase("SelectUseCard");

                                Debug.Log("Phase: SelectUseCard");
                            }
                            else if (numberOfReconnaissanceUnit > 1)
                            {
                                GameObject.Find("PhaseManager")
                                    .GetComponent<PhaseManager>()
                                    .SetNextPhase("SelectReconnaissanceUnit");

                                Debug.Log("Phase: SelectReconnaissanceUnit");
                            }
                            else
                            {
                                dragGameObjectDraggable.PlaceholderParent =
                                    dragGameObjectDraggable.ParentToReturnTo;

                                Debug.Log("Don't exist ReconnaissanceUnit.");

                                return;
                            }

                            break;

                        default:
                            dragGameObjectDraggable.PlaceholderParent =
                                dragGameObjectDraggable.ParentToReturnTo;

                            Debug.Log("Don't exist this card type.");

                            return;
                    }

                    dragGameObjectDraggable.ParentToReturnTo = transform;
                    await Task.Delay(TimeSpan.FromSeconds(1.0f));
                    dragGameObject.transform.SetParent(GameObject.Find("Graveyard").transform);
                    dragGameObject.gameObject.SetActive(false);
                }
            );
    }
}