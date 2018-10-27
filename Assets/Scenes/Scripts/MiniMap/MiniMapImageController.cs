using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Asset.Scripts.MiniMap;
using UniRx.Triggers;
using UniRx;
using System;


public class MiniMapImageController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private IPhaseType _phaseType;
    private float _time;
    [SerializeField]private float _angularFrequency = 5f;
    [SerializeField]private static readonly float deltaTime = 0.0333f;

    private void Start()
    {
        ObservableEventTrigger observableEventTrigger = gameObject.AddComponent<ObservableEventTrigger>();

        int posX = GetComponent<MiniMapImageInstancePosition>().PosX;
        int posZ = GetComponent<MiniMapImageInstancePosition>().PosZ;

        observableEventTrigger.OnPointerClickAsObservable()
            .Subscribe();

        IObservable<string> summonPhase = PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase => phase != "SelectMiniMapPositionUnitSummon");

        PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase => phase == "SelectUseCard")
            .Subscribe(_ => GetComponent<Image>().color = Color.white);

        PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase =>
                phase == "SelectMiniMapPositionUnitSummon" &&
                ((posX == 1 && posZ == 0) || (posX == 2 && posZ == 0) || (posX == 3 && posZ == 0))
            )
            .Subscribe(_ =>
                {
                    Observable.Interval(TimeSpan.FromSeconds(deltaTime))
                        .TakeUntil(summonPhase)
                        .Subscribe(__ =>
                        {
                            GetComponent<Image>().color = Color.green;
                            _time += _angularFrequency * deltaTime;
                            var color = GetComponent<Image>().color;
                            color.a = Mathf.Sin(_time) * 0.5f + 0.5f;
                            GetComponent<Image>().color = color;
                        }).AddTo(this);
                }
            );
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