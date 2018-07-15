using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitAttackManager : MonoBehaviour
{
	private UnitStatus _attackerStatus;
	
	private UnitStatus _targetStatus;
	
	private List<GameObject> _attackerUnitList = null;
	private List<GameObject> _targetUnitList = null;
	
	
	
	public struct AttackerAndTarget
	{
		public GameObject Attacker;
		public GameObject Target;
	}
	
	private List<AttackerAndTarget> _attackerAndTargetList = new List<AttackerAndTarget>();

	public void AttackCommand(GameObject attacker, GameObject target)
	{
		_attackerStatus = attacker.GetComponent<UnitStatus>().GetUnitStatus();
		_targetStatus = target.GetComponent<UnitStatus>().GetUnitStatus();

		_targetStatus.HitPoint -= (_attackerStatus.AttackPoint - _targetStatus.DefensPoint);
		
		_targetStatus.SetUnitStatus(_targetStatus);
	}

	public bool ExistAttackTargetUnit()
	{
		_attackerUnitList = GameObject.Find("UnitManager").GetComponent<UnitManager>().GetUnitList();
//		foreach (GameObject attackerUnit in _attackerUnitList)
//		{
//			Debug.Log("attacker: " + attackerUnit.name);
//		}

		_targetUnitList = GameObject.Find("UnitManager").GetComponent<UnitManager>().GetUnitList();

		foreach (GameObject attackerUnit in _attackerUnitList)
		{
			int attackerUnitPositionX = Mathf.RoundToInt(attackerUnit.transform.position.x);
			int attackerUnitPositionZ = Mathf.RoundToInt(attackerUnit.transform.position.z);
			
			//Debug.Log("attacker: " + attackerUnit.name);
			
			foreach (GameObject targetUnit in _targetUnitList)
			{
				int targetUnitPositionX = Mathf.RoundToInt(targetUnit.transform.position.x);
				int targetUnitPositionZ = Mathf.RoundToInt(targetUnit.transform.position.z);
				
				int absX = Math.Abs(targetUnitPositionX - attackerUnitPositionX);
				int absZ = Math.Abs(targetUnitPositionZ - attackerUnitPositionZ);
				
				//Debug.Log("target: " + targetUnit.name);

				if (attackerUnit.CompareTag("ProximityAttackUnit") && (absX == 0 && absZ == 1 || absX == 1 && absZ == 0))
				{
					_attackerAndTargetList.Add(new AttackerAndTarget() {Attacker = attackerUnit, Target = targetUnit});
				}
			}
		}

		return _attackerAndTargetList != null;
	}

	public List<AttackerAndTarget> GetAttackerAndTargetList()
	{
		return _attackerAndTargetList;
	}
}
