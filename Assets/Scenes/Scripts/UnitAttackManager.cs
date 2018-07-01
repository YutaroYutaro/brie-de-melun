using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackManager : MonoBehaviour
{
	private UnitStatus _attackerStatus;
	
	private UnitStatus _targetStatus;

	public void AttackCommand(GameObject attacker, GameObject target)
	{
		_attackerStatus = attacker.GetComponent<UnitStatus>().GetUnitStatus();
		_targetStatus = target.GetComponent<UnitStatus>().GetUnitStatus();

		_targetStatus.HitPoint -= (_attackerStatus.AttackPoint - _targetStatus.DefensPoint);
		
		_targetStatus.SetUnitStatus(_targetStatus);
	}
}
