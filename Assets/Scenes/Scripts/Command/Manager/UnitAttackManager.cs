using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAttackManager : MonoBehaviour
{
    public struct AttackerAndTarget
    {
        public GameObject Attacker;
        public List<GameObject> Target;
    }

    private List<AttackerAndTarget> _attackerAndTargetList;

    private AttackerAndTarget _selectedAttackerAndTarget;

    private GameObject _selectedAttacker;
    private GameObject _selectedSurpriseAttacker;

    private void Start()
    {
        _attackerAndTargetList = new List<AttackerAndTarget>();
    }

    public bool ExistAttackTargetUnit()
    {
        _attackerAndTargetList.Clear();

        Transform player1UnitChildren = GameObject.Find("Player1Units").transform;
        Transform player2UnitChildren = GameObject.Find("Player2Units").transform;

        foreach (Transform player1UnitChild in player1UnitChildren)
        {
            AttackerAndTarget attackerAndTarget;
            attackerAndTarget.Target = new List<GameObject>();

            foreach (Transform player2UnitChild in player2UnitChildren)
            {
                int absX = Math.Abs(
                    player2UnitChild.GetComponent<UnitOwnIntPosition>().PosX
                    - player1UnitChild.GetComponent<UnitOwnIntPosition>().PosX
                );
                int absZ = Math.Abs(
                    player2UnitChild.GetComponent<UnitOwnIntPosition>().PosZ
                    - player1UnitChild.GetComponent<UnitOwnIntPosition>().PosZ
                );

                if (player1UnitChild.CompareTag("ProximityAttackUnit") &&
                    (absX == 0 && absZ == 1 || absX == 1 && absZ == 0) &&
                    player2UnitChild.gameObject.activeSelf)
                {
                    attackerAndTarget.Target.Add(player2UnitChild.gameObject);
                }
            }

            if (attackerAndTarget.Target.Count != 0)
            {
                attackerAndTarget.Attacker = player1UnitChild.gameObject;
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

    public GameObject SelectedAttacker
    {
        set { _selectedAttacker = value; }
    }

    public void SetSurpriseAttacker(GameObject surpriseAttackTarget)
    {
        _selectedSurpriseAttacker = surpriseAttackTarget;
    }

    public void MiniMapUnitAttack(GameObject targetUnit)
    {
        _selectedAttacker.GetComponent<UnitAttack>().MiniMapClickUnitAttack(targetUnit);
    }

    public void SurpriseAttack(GameObject targetUnit)
    {
        _selectedSurpriseAttacker.GetComponent<UnitAttack>().SurpriseAttack(targetUnit);
    }
}
