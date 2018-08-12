using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SummonUnitTypeDefine;

public class MiniMapImageController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private MiniMapImageInstancePosition _miniMapImageInstancePosition = null;


    private string _nowPhase = null;


    public void OnPointerClick(PointerEventData eventData)
    {
        _nowPhase = GameObject.Find("PhaseManager").GetComponent<PhaseManager>().GetNowPhase();

        _miniMapImageInstancePosition = eventData.pointerPress.GetComponent<MiniMapImageInstancePosition>();

        if (_nowPhase == "SelectMoveUnit")
        {
            List<GameObject> unitList = GameObject.Find("UnitManager").GetComponent<UnitManager>().GetMyUnitList();

            for (int i = 0; i < unitList.Count; i++)
            {
                int unitPositionX = Mathf.RoundToInt(unitList[i].transform.position.x);
                int unitPositionZ = Mathf.RoundToInt(unitList[i].transform.position.z);

                if (unitPositionX == _miniMapImageInstancePosition.PosX &&
                    unitPositionZ == _miniMapImageInstancePosition.PosZ)
                {
                    GameObject.Find("UnitMoveManager").GetComponent<UnitMoveManager>().SetMoveUnit(unitList[i]);
                    GameObject.Find("PhaseManager").GetComponent<PhaseManager>().SetNextPhase("SelectDestination");
                    Debug.Log("Phase: SelectDestination");
                    break;
                }
            }
        }
        else if (_nowPhase == "SelectDestination")
        {
            GameObject.Find("UnitMoveManager").GetComponent<UnitMoveManager>()
                .MiniMapUnitMove(_miniMapImageInstancePosition.PosX, _miniMapImageInstancePosition.PosZ);
            GameObject.Find("PhaseManager").GetComponent<PhaseManager>().SetNextPhase("SelectUseCard");
            Debug.Log("Phase: SelectUseCard");
            GameObject.Find("UnitMoveManager").GetComponent<UnitMoveManager>().SetMoveUnit(null);
        }
        else if (_nowPhase == "SelectAttackerUnit")
        {
            UnitAttackManager unitAttackManager =
                GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>();

            List<UnitAttackManager.AttackerAndTarget> attackerAndTargetList =
                unitAttackManager.GetAttackerAndTargetList();

            foreach (UnitAttackManager.AttackerAndTarget attackerAndTarget in attackerAndTargetList)
            {
                int unitPositionX = Mathf.RoundToInt(attackerAndTarget.Attacker.transform.position.x);
                int unitPositionZ = Mathf.RoundToInt(attackerAndTarget.Attacker.transform.position.z);

                if (unitPositionX == _miniMapImageInstancePosition.PosX &&
                    unitPositionZ == _miniMapImageInstancePosition.PosZ)
                {
                    unitAttackManager.SetSelectedAttackerAndTargetUnit(attackerAndTarget);
                    Debug.Log("Debug: Set Attacker Unit");

                    if (attackerAndTarget.Target.Count == 1)
                    {
                        unitAttackManager.MiniMapUnitAttack(attackerAndTarget.Target[0]);
                        Debug.Log("Phase: SelectUseCard");
                        GameObject.Find("PhaseManager").GetComponent<PhaseManager>().SetNextPhase("SelectUseCard");
                    }
                    else
                    {
                        Debug.Log("Phase: SelectAttackTargetUnit");
                        GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                            .SetNextPhase("SelectAttackTargetUnit");
                    }

                    break;
                }
            }
        }
        else if (_nowPhase == "SelectAttackTargetUnit")
        {
            UnitAttackManager unitAttackManager =
                GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>();

            List<GameObject> selectedTargetList =
                unitAttackManager.GetSelectedAttackerAndTargetUnit().Target;

            foreach (GameObject selectedTarget in selectedTargetList)
            {
                int unitPositionX = Mathf.RoundToInt(selectedTarget.transform.position.x);
                int unitPositionZ = Mathf.RoundToInt(selectedTarget.transform.position.z);

                if (unitPositionX == _miniMapImageInstancePosition.PosX &&
                    unitPositionZ == _miniMapImageInstancePosition.PosZ)
                {
                    unitAttackManager.MiniMapUnitAttack(selectedTarget);
                    Debug.Log("Phase: SelectUseCard");
                    GameObject.Find("PhaseManager").GetComponent<PhaseManager>().SetNextPhase("SelectUseCard");
                }
            }
        }

        switch (_nowPhase)
        {
            case "SelectMiniMapPositionUnitSummon":
                int posX = _miniMapImageInstancePosition.PosX;
                int posZ = _miniMapImageInstancePosition.PosZ;

                if (posZ == 0 && (posX == 1 || posX == 2 || posX == 3))
                {
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
                                Debug.Log("Exist Unit");
                                existUnit1 = true;
                            }
                            else if (Mathf.RoundToInt(player1UnitChild.position.x) == 2 &&
                                     Mathf.RoundToInt(player1UnitChild.position.z) == 0)
                            {
                                Debug.Log("Exist Unit");
                                existUnit2 = true;
                            }
                            else if (Mathf.RoundToInt(player1UnitChild.position.x) == 3 &&
                                     Mathf.RoundToInt(player1UnitChild.position.z) == 0)
                            {
                                Debug.Log("Exist Unit");
                                existUnit3 = true;
                            }
                        }

                        if (!existUnit1 && posX == 1 || !existUnit2 && posX == 2 || !existUnit3 && posX == 3)
                        {
                            switch (GameObject.Find("UnitSummonGenerator").GetComponent<UnitSummonGenerator>()
                                .SummonUnitType)
                            {
                                case SummonUnitTypeDefine.SummonUnitType.PROXIMITY:
                                    GameObject.Find("UnitSummonGenerator").GetComponent<UnitSummonGenerator>()
                                        .SummonProximityAttackUnit(posX, posZ);
                                    break;
                                case SummonUnitTypeDefine.SummonUnitType.REMOTE:
                                    break;
                                case SummonUnitTypeDefine.SummonUnitType.RECONNAISSANCE:
                                    GameObject.Find("UnitSummonGenerator").GetComponent<UnitSummonGenerator>()
                                        .SummonReconnaissanceUnit(posX, posZ);
                                    break;
                            }

                            Debug.Log("Phase: SelectUseCard");
                            GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                                .SetNextPhase("SelectUseCard");
                        }
                    }
                    else
                    {
                        switch (GameObject.Find("UnitSummonGenerator").GetComponent<UnitSummonGenerator>()
                            .SummonUnitType)
                        {
                            case SummonUnitTypeDefine.SummonUnitType.PROXIMITY:
                                GameObject.Find("UnitSummonGenerator").GetComponent<UnitSummonGenerator>()
                                    .SummonProximityAttackUnit(posX, posZ);
                                break;
                            case SummonUnitTypeDefine.SummonUnitType.REMOTE:
                                break;
                            case SummonUnitTypeDefine.SummonUnitType.RECONNAISSANCE:
                                GameObject.Find("UnitSummonGenerator").GetComponent<UnitSummonGenerator>()
                                    .SummonReconnaissanceUnit(posX, posZ);
                                break;
                        }

                        Debug.Log("Phase: SelectUseCard");
                        GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                            .SetNextPhase("SelectUseCard");
                    }
                }

                break;
            case "SelectReconnaissanceUnit":
                foreach (Transform unit in GameObject.Find("Player1Units").transform)
                {
                    if (unit.gameObject.CompareTag("ReconnaissanceUnit") &&
                        _miniMapImageInstancePosition.PosX == unit.GetComponent<UnitOwnIntPosition>().PosX &&
                        _miniMapImageInstancePosition.PosZ == unit.GetComponent<UnitOwnIntPosition>().PosZ
                    )
                    {
                        unit.GetComponent<UnitReconnaissanceController>().UnitReconnaissance();
                        Debug.Log("Phase: SelectUseCard");
                        GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                            .SetNextPhase("SelectUseCard");
                    }
                }
                break;
        }

        Debug.Log("PosX: " + _miniMapImageInstancePosition.PosX +
                  " PosZ: " + _miniMapImageInstancePosition.PosZ);
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