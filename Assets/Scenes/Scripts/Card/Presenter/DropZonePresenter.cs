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
                            AttackCardController attackCardController = new AttackCardController();
                            _isSetBool = attackCardController.SetCardTypePhase();

                            //攻撃できるユニットが存在するか判定
                            if (_isSetBool == false)
                            {
                                dragGameObjectDraggable.PlaceholderParent =
                                    dragGameObjectDraggable.ParentToReturnTo;

                                Debug.Log("Don't exist Attacker or Target.");

                                return;
                            }

                            break;

                        case "SummonCard":
                            SummonCardController summonCardController = new SummonCardController();
                            _isSetBool = summonCardController.SetCardTypePhase();

                            if (_isSetBool == false)
                            {
                                dragGameObjectDraggable.PlaceholderParent =
                                    dragGameObjectDraggable.ParentToReturnTo;

                                Debug.Log("Don't exist Attacker or Target.");

                                return;
                            }

                            GameObject.Find("UnitSummonGenerator").GetComponent<UnitSummonGenerator>().SummonUnitType =
                                dragGameObject.GetComponent<SummonUnitType>().SummonunitType;

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