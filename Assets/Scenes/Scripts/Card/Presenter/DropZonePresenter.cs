using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;
using System.Linq;
using System.Threading.Tasks;
using Asset.Scripts.Cards;

public class DropZonePresenter : MonoBehaviour
{
    private ICardType _cardType;

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
                    GameObject dragGameObject = eventData.pointerDrag;
                    Draggable dragGameObjectDraggable = dragGameObject.GetComponent<Draggable>();

                    switch (dragGameObject.tag)
                    {
                        case "MoveCard":
                            _cardType = new MoveCardController();

                            break;

                        case "AttackCard":
                            _cardType = new AttackCardController();

                            break;

                        case "SummonCard":
                            _cardType = new SummonCardController();

                            if(_cardType.SetCardTypePhase() == false) break;

                            GameObject.Find("UnitSummonGenerator").GetComponent<UnitSummonGenerator>().SummonUnitType =
                                dragGameObject.GetComponent<SummonUnitType>().SummonunitType;

                            break;

                        case "ReconnaissanceCard":
                            _cardType = new ReconnaissanceCardController();

                            break;

                        default:
                            Debug.Log("Don't exist this card type.");

                            return;
                    }

                    if (_cardType.SetCardTypePhase() == false)
                    {
                        dragGameObjectDraggable.PlaceholderParent =
                            dragGameObjectDraggable.ParentToReturnTo;
                        return;
                    }

                    ManaModel.Instance.ManaReactiveProperty.Value -=
                        eventData.pointerDrag.GetComponent<ManaOfCard>().Mana;

                    dragGameObjectDraggable.ParentToReturnTo = transform;
                    await Task.Delay(TimeSpan.FromSeconds(1.0f));
                    dragGameObject.transform.SetParent(GameObject.Find("Graveyard").transform);
                    dragGameObject.gameObject.SetActive(false);
                }
            );
    }
}