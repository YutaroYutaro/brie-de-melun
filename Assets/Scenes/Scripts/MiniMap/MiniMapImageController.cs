using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Asset.Scripts.MiniMap;
using UniRx.Triggers;
using UniRx;


public class MiniMapImageController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private IPhaseType _phaseType;

    private void Start()
    {
        ObservableEventTrigger observableEventTrigger = gameObject.AddComponent<ObservableEventTrigger>();

        observableEventTrigger.OnPointerClickAsObservable()
            .Subscribe();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int miniMapPosX = eventData.pointerPress.GetComponent<MiniMapImageInstancePosition>().PosX;
        int miniMapPosZ = eventData.pointerPress.GetComponent<MiniMapImageInstancePosition>().PosZ;

        switch (PhaseManager.Instance.PhaseReactiveProperty.Value)
        {
            case "SelectMoveUnit":
                _phaseType = new SelectMoveUnitPhase();

                break;

            case "SelectDestination":
                _phaseType = new SelectDestinationPhase();

                break;

            case "SelectAttackerUnit":
                _phaseType = new SelectAttackerUnitPhase();

                break;

            case "SelectAttackTargetUnit":
                _phaseType = new SelectAttackTargetUnitPhase();

                break;

            case "SelectMiniMapPositionUnitSummon":
                _phaseType = new SelectMiniMapPositionUnitSummonPhase();

                break;

            case "SelectReconnaissanceUnit":
                _phaseType = new SelectReconnaissanceUnitPhase();

                break;

            default:
                Debug.Log("PosX: " + miniMapPosX +
                          " PosZ: " + miniMapPosZ);
                return;
        }

        _phaseType.PhaseController(miniMapPosX, miniMapPosZ);

        Debug.Log("PosX: " + miniMapPosX +
                  " PosZ: " + miniMapPosZ);
    }

    //マップオブジェクトにマウスがホバーしたときに色を変更
    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.pointerEnter.GetComponent<Image>().color = Color.red;
    }

    //ホバーが解除されたら元の色に戻す
    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.pointerEnter.GetComponent<Image>().color = Color.white;
    }
}