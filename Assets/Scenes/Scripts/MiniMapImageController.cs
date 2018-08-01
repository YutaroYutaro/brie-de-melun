using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniMapImageController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private MiniMapImageInstancePosition _miniMapImageInstancePosition = null;

    //private List<GameObject> _unitList = null;

    private string _nowPhase = null;

    //private UnitAttackManager _unitAttackManager = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Clicked");

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
                    //Debug.Log(_unitList[i].gameObject.name);
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

        //Debug.Log(this.gameObject.GetInstanceID());
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