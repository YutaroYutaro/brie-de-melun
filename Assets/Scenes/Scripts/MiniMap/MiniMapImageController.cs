﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Asset.Scripts.MiniMap;

public class MiniMapImageController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private IPhaseType _phaseType;

    public void OnPointerClick(PointerEventData eventData)
    {
        int miniMapPosX = eventData.pointerPress.GetComponent<MiniMapImageInstancePosition>().PosX;
        int miniMapPosZ = eventData.pointerPress.GetComponent<MiniMapImageInstancePosition>().PosZ;

        switch (PhaseManager.Instance.PhaseReactiveProperty.Value)
        {
            case "SelectMoveUnit":
                _phaseType = new SelectMoveUnitPhase();
                _phaseType.PhaseController(miniMapPosX, miniMapPosZ);

                break;

            case "SelectDestination":
                _phaseType = new SelectDestinationPhase();
                _phaseType.PhaseController(miniMapPosX, miniMapPosZ);

                break;

            case "SelectAttackerUnit":
                _phaseType = new SelectAttackerUnitPhase();
                _phaseType.PhaseController(miniMapPosX, miniMapPosZ);

                break;

            case "SelectAttackTargetUnit":
                _phaseType = new SelectAttackTargetUnitPhase();
                _phaseType.PhaseController(miniMapPosX, miniMapPosZ);

                break;

            case "SelectMiniMapPositionUnitSummon":
                if (miniMapPosZ == 0 && (miniMapPosX == 1 || miniMapPosX == 2 || miniMapPosX == 3))
                {
                    if (GameObject.Find("Player1Units").transform.childCount != 0)
                    {
                        Transform player1UnitChildren = GameObject.Find("Player1Units").transform;

                        foreach (Transform player1UnitChild in player1UnitChildren)
                        {
                            if (player1UnitChild.GetComponent<UnitOwnIntPosition>().PosX == miniMapPosX &&
                                player1UnitChild.GetComponent<UnitOwnIntPosition>().PosZ == miniMapPosZ)
                            {
                                Debug.Log("Exist Unit");
                                return;
                            }
                        }
                    }

                    switch (GameObject.Find("UnitSummonGenerator")
                        .GetComponent<UnitSummonGenerator>()
                        .SummonUnitType
                    )
                    {
                        case SummonUnitTypeDefine.SummonUnitType.PROXIMITY:
                            GameObject.Find("UnitSummonGenerator")
                                .GetComponent<UnitSummonGenerator>()
                                .SummonProximityAttackUnit(
                                    miniMapPosX,
                                    miniMapPosZ
                                );

                            break;

                        case SummonUnitTypeDefine.SummonUnitType.REMOTE:
                            GameObject.Find("UnitSummonGenerator")
                                .GetComponent<UnitSummonGenerator>()
                                .SummonRemoteAttackUnit(
                                    miniMapPosX,
                                    miniMapPosZ
                                );

                            break;

                        case SummonUnitTypeDefine.SummonUnitType.RECONNAISSANCE:
                            GameObject.Find("UnitSummonGenerator")
                                .GetComponent<UnitSummonGenerator>()
                                .SummonReconnaissanceUnit(
                                    miniMapPosX,
                                    miniMapPosZ
                                );

                            break;
                    }

                    GameObject.Find("PhaseManager")
                        .GetComponent<PhaseManager>()
                        .SetNextPhase("SelectUseCard");

                    Debug.Log("Phase: SelectUseCard");
                }

                break;

            case "SelectReconnaissanceUnit":
                foreach (Transform unit in GameObject.Find("Player1Units").transform)
                {
                    if (unit.gameObject.CompareTag("ReconnaissanceUnit") &&
                        miniMapPosX == unit.GetComponent<UnitOwnIntPosition>().PosX &&
                        miniMapPosZ == unit.GetComponent<UnitOwnIntPosition>().PosZ
                    )
                    {
                        unit.GetComponent<UnitReconnaissanceController>()
                            .UnitReconnaissance();

                        GameObject.Find("PhaseManager")
                            .GetComponent<PhaseManager>()
                            .SetNextPhase("SelectUseCard");

                        Debug.Log("Phase: SelectUseCard");
                    }
                }

                break;

            default:
                Debug.Log("PosX: " + miniMapPosX +
                          " PosZ: " + miniMapPosZ);
                return;
        }

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