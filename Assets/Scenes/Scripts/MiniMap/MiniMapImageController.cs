using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SummonUnitTypeDefine;

public class MiniMapImageController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        string nowPhase = GameObject.Find("PhaseManager").GetComponent<PhaseManager>().GetNowPhase();

        int miniMapPosX = eventData.pointerPress.GetComponent<MiniMapImageInstancePosition>().PosX;
        int miniMapPosZ = eventData.pointerPress.GetComponent<MiniMapImageInstancePosition>().PosZ;

        switch (nowPhase)
        {
            case "SelectMoveUnit":
                Transform player1UnitChildren = GameObject.Find("Player1Units").transform;

                foreach (Transform player1UnitChild in player1UnitChildren)
                {
                    if (player1UnitChild.GetComponent<UnitOwnIntPosition>().PosX == miniMapPosX &&
                        player1UnitChild.GetComponent<UnitOwnIntPosition>().PosZ == miniMapPosZ)
                    {
                        GameObject.Find("UnitMoveManager")
                            .GetComponent<UnitMoveManager>()
                            .SetMoveUnit(player1UnitChild.gameObject);

                        GameObject.Find("PhaseManager")
                            .GetComponent<PhaseManager>()
                            .SetNextPhase("SelectDestination");

                        Debug.Log("Phase: SelectDestination");

                        break;
                    }
                }

                break;

            case "SelectDestination":
                GameObject.Find("UnitMoveManager")
                    .GetComponent<UnitMoveManager>()
                    .MiniMapUnitMove(
                        miniMapPosX,
                        miniMapPosZ
                    );

                GameObject.Find("PhaseManager")
                    .GetComponent<PhaseManager>()
                    .SetNextPhase("SelectUseCard");

                Debug.Log("Phase: SelectUseCard");

                GameObject.Find("UnitMoveManager")
                    .GetComponent<UnitMoveManager>()
                    .SetMoveUnit(null);

                break;

            case "SelectAttackerUnit":
                UnitAttackManager unitAttackManager =
                    GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>();

                List<UnitAttackManager.AttackerAndTarget> attackerAndTargetList =
                    unitAttackManager.GetAttackerAndTargetList();

                foreach (UnitAttackManager.AttackerAndTarget attackerAndTarget in attackerAndTargetList)
                {
                    if (attackerAndTarget.Attacker.GetComponent<UnitOwnIntPosition>().PosX == miniMapPosX &&
                        attackerAndTarget.Attacker.GetComponent<UnitOwnIntPosition>().PosZ == miniMapPosZ)
                    {
                        unitAttackManager.SetSelectedAttackerAndTargetUnit(attackerAndTarget);

                        Debug.Log("Debug: Set Attacker Unit");

                        if (attackerAndTarget.Target.Count == 1)
                        {
                            unitAttackManager.MiniMapUnitAttack(attackerAndTarget.Target[0]);

                            GameObject.Find("PhaseManager")
                                .GetComponent<PhaseManager>()
                                .SetNextPhase("SelectUseCard");

                            Debug.Log("Phase: SelectUseCard");
                        }
                        else
                        {
                            GameObject.Find("PhaseManager")
                                .GetComponent<PhaseManager>()
                                .SetNextPhase("SelectAttackTargetUnit");

                            Debug.Log("Phase: SelectAttackTargetUnit");
                        }

                        break;
                    }
                }

                break;

            case "SelectAttackTargetUnit":
                unitAttackManager =
                    GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>();

                List<GameObject> selectedTargetList =
                    unitAttackManager.GetSelectedAttackerAndTargetUnit().Target;

                foreach (GameObject selectedTarget in selectedTargetList)
                {
                    if (selectedTarget.GetComponent<UnitOwnIntPosition>().PosX == miniMapPosX &&
                        selectedTarget.GetComponent<UnitOwnIntPosition>().PosZ == miniMapPosZ)
                    {
                        unitAttackManager.MiniMapUnitAttack(selectedTarget);

                        GameObject.Find("PhaseManager")
                            .GetComponent<PhaseManager>()
                            .SetNextPhase("SelectUseCard");

                        Debug.Log("Phase: SelectUseCard");
                    }
                }

                break;

            case "SelectMiniMapPositionUnitSummon":
                if (miniMapPosZ == 0 && (miniMapPosX == 1 || miniMapPosX == 2 || miniMapPosX == 3))
                {
                    if (GameObject.Find("Player1Units").transform.childCount != 0)
                    {
                        player1UnitChildren = GameObject.Find("Player1Units").transform;

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