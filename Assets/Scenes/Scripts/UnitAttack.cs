using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FogDefine;

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

        int[,] playerTwoFogMapState =
            GameObject.Find("FogManager").GetComponent<FogManager>().GetPlayerTwoFogMapState();

        if (playerTwoFogMapState[
                GetComponent<UnitOwnIntPosition>().PosX,
                GetComponent<UnitOwnIntPosition>().PosZ
            ]
            == Fog.FOG_EXIST)
        {
            _targetStatus.HitPoint -= _attackerStatus.AttackPoint;
            Debug.Log("Attack damage: " + _attackerStatus.AttackPoint);
            GameObject.Find("FogManager").GetComponent<FogManager>().SetPlayerTwoFogMapState(
                GetComponent<UnitOwnIntPosition>().PosX,
                GetComponent<UnitOwnIntPosition>().PosZ,
                Fog.FOG_NOT_EXIST
            );
        }
        else
        {
            _targetStatus.HitPoint -= (_attackerStatus.AttackPoint - _targetStatus.DefensPoint);
            Debug.Log("Attack damage: " + (_attackerStatus.AttackPoint - _targetStatus.DefensPoint));
        }

        _targetStatus.SetUnitStatus(_targetStatus);
    }

    public async void SurpriseAttack(GameObject targetUnit)
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

        GameObject.Find("FogManager").GetComponent<FogManager>().SetPlayerOneFogMapState(
            GetComponent<UnitOwnIntPosition>().PosX,
            GetComponent<UnitOwnIntPosition>().PosZ,
            Fog.FOG_NOT_EXIST
            );
        GameObject.Find("FogManager").GetComponent<FogManager>().ClearFog(
            GetComponent<UnitOwnIntPosition>().PosX,
            GetComponent<UnitOwnIntPosition>().PosZ
            );
        GameObject.Find("FogManager").GetComponent<FogManager>().SetPlayerTwoFogMapState(
            targetUnit.GetComponent<UnitOwnIntPosition>().PosX,
            targetUnit.GetComponent<UnitOwnIntPosition>().PosZ,
            Fog.FOG_NOT_EXIST
            );


        _targetStatus.HitPoint -= _attackerStatus.AttackPoint;
        Debug.Log("Attacked damage: " + _attackerStatus.AttackPoint);

        _targetStatus.SetUnitStatus(_targetStatus);
    }
}