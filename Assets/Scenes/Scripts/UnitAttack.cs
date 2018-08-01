using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    private UnitStatus _attackerStatus;
    private UnitStatus _targetStatus;

    public async void MiniMapClickUnitAttack(GameObject targetUnit)
    {
        _attackerStatus = this.GetComponent<UnitStatus>().GetUnitStatus();
        _targetStatus = targetUnit.GetComponent<UnitStatus>().GetUnitStatus();

        targetUnit.SetActive(false);
        await Task.Delay(TimeSpan.FromSeconds(0.1f));
        targetUnit.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(0.2f));
        targetUnit.SetActive(false);
        await Task.Delay(TimeSpan.FromSeconds(0.1f));
        targetUnit.SetActive(true);
        await Task.Delay(TimeSpan.FromSeconds(0.2f));
        targetUnit.SetActive(false);
        await Task.Delay(TimeSpan.FromSeconds(0.1f));
        targetUnit.SetActive(true);
        //await Task.Delay(TimeSpan.FromSeconds(0.1f));


        _targetStatus.HitPoint -= (_attackerStatus.AttackPoint - _targetStatus.DefensPoint);

        _targetStatus.SetUnitStatus(_targetStatus);
    }
}