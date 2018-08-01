using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAttackManager : MonoBehaviour
{
    private List<GameObject> _attackerUnitList = null;
    private List<GameObject> _targetUnitList = null;


    public struct AttackerAndTarget
    {
        public GameObject Attacker;
        public List<GameObject> Target;
    }

    private List<AttackerAndTarget> _attackerAndTargetList = new List<AttackerAndTarget>();

    private AttackerAndTarget _selectedAttackerAndTarget;


    public bool ExistAttackTargetUnit()
    {
        _attackerAndTargetList.Clear();

        _attackerUnitList = GameObject.Find("UnitManager").GetComponent<UnitManager>().GetMyUnitList();

        _targetUnitList = GameObject.Find("UnitManager").GetComponent<UnitManager>().GetEnemyUnitList();

        foreach (GameObject attackerUnit in _attackerUnitList)
        {
            AttackerAndTarget attackerAndTarget;
            attackerAndTarget.Target = new List<GameObject>();

            int attackerUnitPositionX = Mathf.RoundToInt(attackerUnit.transform.position.x);
            int attackerUnitPositionZ = Mathf.RoundToInt(attackerUnit.transform.position.z);

            foreach (GameObject targetUnit in _targetUnitList)
            {
                int targetUnitPositionX = Mathf.RoundToInt(targetUnit.transform.position.x);
                int targetUnitPositionZ = Mathf.RoundToInt(targetUnit.transform.position.z);

                int absX = Math.Abs(targetUnitPositionX - attackerUnitPositionX);
                int absZ = Math.Abs(targetUnitPositionZ - attackerUnitPositionZ);

                if (attackerUnit.CompareTag("ProximityAttackUnit") &&
                    (absX == 0 && absZ == 1 || absX == 1 && absZ == 0))
                {
                    attackerAndTarget.Target.Add(targetUnit);
                }
            }

            if (attackerAndTarget.Target.Count != 0)
            {
                attackerAndTarget.Attacker = attackerUnit;
                _attackerAndTargetList.Add(attackerAndTarget);
            }
        }

        return _attackerAndTargetList.Count != 0;
    }

    public List<AttackerAndTarget> GetAttackerAndTargetList()
    {
        return _attackerAndTargetList;
    }

    public void SetSelectedAttackerAndTargetUnit(AttackerAndTarget attackerAndTarget)
    {
        _selectedAttackerAndTarget = attackerAndTarget;
    }

    public AttackerAndTarget GetSelectedAttackerAndTargetUnit()
    {
        return _selectedAttackerAndTarget;
    }

    public void ClearGetAttackerAndTargetList()
    {
        _attackerAndTargetList.Clear();
    }

    public void MiniMapUnitAttack(GameObject targetUnit)
    {
        _selectedAttackerAndTarget.Attacker.GetComponent<UnitAttack>().MiniMapClickUnitAttack(targetUnit);
    }
}