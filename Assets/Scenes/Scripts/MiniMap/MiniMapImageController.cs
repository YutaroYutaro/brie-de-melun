using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Asset.Scripts.MiniMap;
using UniRx.Triggers;
using UniRx;
using System;
using System.Collections.Generic;


public class MiniMapImageController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private IPhaseType _phaseType;
    private float _time;
    [SerializeField] private float _angularFrequency = 5f;
    [SerializeField] private static readonly float deltaTime = 0.0333f;

    private int _miniMapPosX;
    private int _miniMapPosZ;

    private GameObject _onUnit;
    [SerializeField] private GameObject _popUp;

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
            .Where(phase => phase == "SelectUseCard" || phase == "EnemyTurn")
            .Subscribe(_ => GetComponent<Image>().color = Color.white);

        PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase =>
                phase == "SelectMiniMapPositionUnitSummon" &&
                ((posX == 1 && posZ == 0) || (posX == 2 && posZ == 0) || (posX == 3 && posZ == 0))
            )
            .Subscribe(_ =>
                {
//                    Observable.Timer(TimeSpan.FromMilliseconds(100));

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

        IObservable<string> selectMoveUnit = PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase => phase != "SelectMoveUnit");

        PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase =>
                phase == "SelectMoveUnit"
            )
            .Subscribe(_ =>
                {
                    Transform player1UnitsChildren = GameObject.Find("Player1Units").transform;
                    foreach (Transform player1UnitsChild in player1UnitsChildren)
                    {
                        if (
                            player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosX == posX &&
                            player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosZ == posZ)
                        {
                            Observable.Interval(TimeSpan.FromSeconds(deltaTime))
                                .TakeUntil(selectMoveUnit)
                                .Subscribe(__ =>
                                {
                                    GetComponent<Image>().color = Color.green;
                                    _time += _angularFrequency * deltaTime;
                                    var color = GetComponent<Image>().color;
                                    color.a = Mathf.Sin(_time) * 0.5f + 0.5f;
                                    GetComponent<Image>().color = color;
                                }).AddTo(this);
                        }
                    }
                }
            );

        IObservable<string> selectDestination = PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase => phase != "SelectDestination");

        PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase =>
                phase == "SelectDestination"
            )
            .Subscribe(_ =>
                {
                    GetComponent<Image>().color = Color.white;

                    int unitPosX = UnitMoveManager.Instance.SelectMoveUnit.GetComponent<UnitOwnIntPosition>().PosX;
                    int unitPosZ = UnitMoveManager.Instance.SelectMoveUnit.GetComponent<UnitOwnIntPosition>().PosZ;

                    if (
                        (unitPosX + 1 == posX && unitPosZ == posZ) ||
                        (unitPosX - 1 == posX && unitPosZ == posZ) ||
                        (unitPosX == posX && unitPosZ + 1 == posZ) ||
                        (unitPosX == posX && unitPosZ - 1 == posZ)
                    )
                    {
                        Observable.Interval(TimeSpan.FromSeconds(deltaTime))
                            .TakeUntil(selectDestination)
                            .Subscribe(__ =>
                            {
                                GetComponent<Image>().color = Color.green;
                                _time += _angularFrequency * deltaTime;
                                var color = GetComponent<Image>().color;
                                color.a = Mathf.Sin(_time) * 0.5f + 0.5f;
                                GetComponent<Image>().color = color;
                            }).AddTo(this);
                    }
                }
            );

        IObservable<string> selectAttackerUnit = PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase => phase != "SelectAttackerUnit");

        PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase =>
                phase == "SelectAttackerUnit"
            )
            .Subscribe(_ =>
                {
                    Transform player1UnitsChildren = GameObject.Find("Player1Units").transform;
                    foreach (Transform player1UnitsChild in player1UnitsChildren)
                    {
                        if (
                            player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosX == posX &&
                            player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosZ == posZ &&
                            !player1UnitsChild.gameObject.CompareTag("ReconnaissanceUnit"))
                        {
                            Observable.Interval(TimeSpan.FromSeconds(deltaTime))
                                .TakeUntil(selectAttackerUnit)
                                .Subscribe(__ =>
                                {
                                    GetComponent<Image>().color = Color.green;
                                    _time += _angularFrequency * deltaTime;
                                    var color = GetComponent<Image>().color;
                                    color.a = Mathf.Sin(_time) * 0.5f + 0.5f;
                                    GetComponent<Image>().color = color;
                                }).AddTo(this);
                        }
                    }
                }
            );

        IObservable<string> selectAttackTargetUnit = PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase => phase != "SelectAttackTargetUnit");

        PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase =>
                phase == "SelectAttackTargetUnit"
            )
            .Subscribe(_ =>
                {
                    GetComponent<Image>().color = Color.white;

                    Observable.Timer(TimeSpan.FromMilliseconds(100));

                    GameObject attacker = UnitAttackManager.Instance.SelectedAttacker;

                    int unitPosX = attacker.GetComponent<UnitOwnIntPosition>().PosX;
                    int unitPosZ = attacker.GetComponent<UnitOwnIntPosition>().PosZ;

                    if (attacker.CompareTag("ProximityAttackUnit") &&
                        ((unitPosX + 1 == posX && unitPosZ == posZ) ||
                         (unitPosX - 1 == posX && unitPosZ == posZ) ||
                         (unitPosX == posX && unitPosZ + 1 == posZ) ||
                         (unitPosX == posX && unitPosZ - 1 == posZ)))
                    {
                        Observable.Interval(TimeSpan.FromSeconds(deltaTime))
                            .TakeUntil(selectAttackTargetUnit)
                            .Subscribe(__ =>
                            {
                                GetComponent<Image>().color = Color.green;
                                _time += _angularFrequency * deltaTime;
                                var color = GetComponent<Image>().color;
                                color.a = Mathf.Sin(_time) * 0.5f + 0.5f;
                                GetComponent<Image>().color = color;
                            }).AddTo(this);
                    }

                    if (attacker.CompareTag("RemoteAttackUnit") &&
                        (
                            (unitPosX + 1 == posX && unitPosZ == posZ) ||
                            (unitPosX + 2 == posX && unitPosZ == posZ) ||
                            (unitPosX - 1 == posX && unitPosZ == posZ) ||
                            (unitPosX - 2 == posX && unitPosZ == posZ) ||
                            (unitPosX == posX && unitPosZ + 1 == posZ) ||
                            (unitPosX == posX && unitPosZ + 2 == posZ) ||
                            (unitPosX == posX && unitPosZ - 1 == posZ) ||
                            (unitPosX == posX && unitPosZ - 2 == posZ) ||
                            (unitPosX + 1 == posX && unitPosZ + 1 == posZ) ||
                            (unitPosX + 1 == posX && unitPosZ - 1 == posZ) ||
                            (unitPosX - 1 == posX && unitPosZ + 1 == posZ) ||
                            (unitPosX - 1 == posX && unitPosZ - 1 == posZ)
                        )
                    )
                    {
                        Observable.Interval(TimeSpan.FromSeconds(deltaTime))
                            .TakeUntil(selectAttackTargetUnit)
                            .Subscribe(__ =>
                            {
                                GetComponent<Image>().color = Color.green;
                                _time += _angularFrequency * deltaTime;
                                var color = GetComponent<Image>().color;
                                color.a = Mathf.Sin(_time) * 0.5f + 0.5f;
                                GetComponent<Image>().color = color;
                            }).AddTo(this);
                    }
                }
            );

        IObservable<string> selectReconnaissanceUnit = PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase => phase != "SelectReconnaissanceUnit");

        PhaseManager.Instance.PhaseReactiveProperty
            .Where(phase =>
                phase == "SelectReconnaissanceUnit"
            )
            .Subscribe(_ =>
                {
                    GetComponent<Image>().color = Color.white;

                    foreach (Transform unit in GameObject.Find("Player1Units").transform)
                    {
                        if (unit.gameObject.CompareTag("ReconnaissanceUnit") &&
                            posX == unit.GetComponent<UnitOwnIntPosition>().PosX &&
                            posZ == unit.GetComponent<UnitOwnIntPosition>().PosZ
                        )
                        {
                            Observable.Interval(TimeSpan.FromSeconds(deltaTime))
                                .TakeUntil(selectReconnaissanceUnit)
                                .Subscribe(__ =>
                                {
                                    GetComponent<Image>().color = Color.green;
                                    _time += _angularFrequency * deltaTime;
                                    var color = GetComponent<Image>().color;
                                    color.a = Mathf.Sin(_time) * 0.5f + 0.5f;
                                    GetComponent<Image>().color = color;
                                }).AddTo(this);
                        }
                    }
                }
            );

        PhaseManager.Instance.PhaseReactiveProperty
            .Subscribe(_ =>
            {
                Transform player1UnitsChildren = GameObject.Find("Player1Units").transform;
                Transform player2UnitsChildren = GameObject.Find("Player2Units").transform;

                foreach (Transform player1UnitsChild in player1UnitsChildren)
                {
                    if (player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosX == posX &&
                        player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosZ == posZ)
                    {
                        _onUnit = player1UnitsChild.gameObject;
                        return;
                    }
                }

                foreach (Transform player2UnitsChild in player2UnitsChildren)
                {
                    if (player2UnitsChild.GetComponent<UnitOwnIntPosition>().PosX == posX &&
                        player2UnitsChild.GetComponent<UnitOwnIntPosition>().PosZ == posZ)
                    {
                        _onUnit = player2UnitsChild.gameObject;
                        return;
                    }
                }

                _onUnit = null;
            });
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int miniMapPosX = eventData.pointerPress.GetComponent<MiniMapImageInstancePosition>().PosX;
        int miniMapPosZ = eventData.pointerPress.GetComponent<MiniMapImageInstancePosition>().PosZ;
        _miniMapPosX = miniMapPosX;
        _miniMapPosZ = miniMapPosZ;

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

        if (_onUnit != null)
        {
            UnitStatus unitStatus = _onUnit.gameObject.GetComponent<UnitStatus>().GetUnitStatus();
            PopUpController.Instance.PopUpOpen();

            String nameText = "";

            switch (_onUnit.tag)
            {
                    case "ProximityAttackUnit":
                        nameText = "スライム";
                        break;
                    case "RemoteAttackUnit":
                        nameText = "スケルトン";
                        break;
                    default:
                        nameText = "スカウト";
                        break;
            }
            PopUpController.Instance.SetPopUpText(
                nameText +
                "\n体力：" + unitStatus.HitPoint +
                "\n攻撃力：" + unitStatus.AttackPoint +
                "\n防御力：" + unitStatus.DefensPoint
            );
        }
    }

    //ホバーが解除されたら元の色に戻す
    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.pointerEnter.GetComponent<Image>().color = Color.white;

        if (_onUnit != null)
        {
            PopUpController.Instance.PopUpClose();
        }
    }
}