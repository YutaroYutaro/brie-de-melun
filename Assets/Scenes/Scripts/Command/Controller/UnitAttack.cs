using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FogDefine;

public class UnitAttack : MonoBehaviour
{
    public async void MiniMapClickUnitAttack(GameObject targetUnit)
    {
        UnitStatus attackerStatus = GetComponent<UnitStatus>().GetUnitStatus();
        UnitStatus targetStatus = targetUnit.GetComponent<UnitStatus>().GetUnitStatus();

        int attackerPosX = GetComponent<UnitOwnIntPosition>().PosX;
        int attackerPosZ = GetComponent<UnitOwnIntPosition>().PosZ;
        int targetPosX = targetUnit.GetComponent<UnitOwnIntPosition>().PosX;
        int targetPosZ = targetUnit.GetComponent<UnitOwnIntPosition>().PosZ;

        GetComponent<UnitRotationController>().UnitRotation(
            targetUnit.GetComponent<UnitOwnIntPosition>().PosX - GetComponent<UnitOwnIntPosition>().PosX,
            targetUnit.GetComponent<UnitOwnIntPosition>().PosZ - GetComponent<UnitOwnIntPosition>().PosZ
        );

        targetUnit.GetComponent<UnitRotationController>().UnitRotation(
            GetComponent<UnitOwnIntPosition>().PosX - targetUnit.GetComponent<UnitOwnIntPosition>().PosX,
            GetComponent<UnitOwnIntPosition>().PosZ - targetUnit.GetComponent<UnitOwnIntPosition>().PosZ
        );

        EnemyUnitController.Instance.UnitAttack(
            GetComponent<ViewId>().UnitViewId,
            targetUnit.GetComponent<UnitOwnIntPosition>().PosX,
            targetUnit.GetComponent<UnitOwnIntPosition>().PosZ
        );

        if (CompareTag("RemoteAttackUnit"))
        {
            if (
                (attackerPosX + 1 == targetPosX && attackerPosZ == targetPosZ) ||
                (attackerPosX - 1 == targetPosX && attackerPosZ == targetPosZ) ||
                (attackerPosX == targetPosX && attackerPosZ + 1 == targetPosZ) ||
                (attackerPosX == targetPosX && attackerPosZ - 1 == targetPosZ)
            )
            {
                GetComponent<UnitAnimator>().IsAttack = true;
                await Task.Delay(TimeSpan.FromSeconds(0.9f));
                GetComponent<UnitAnimator>().IsAttack = false;
            }
            else if (
                (attackerPosX + 2 == targetPosX && attackerPosZ == targetPosZ) ||
                (attackerPosX - 2 == targetPosX && attackerPosZ == targetPosZ) ||
                (attackerPosX == targetPosX && attackerPosZ + 2 == targetPosZ) ||
                (attackerPosX == targetPosX && attackerPosZ - 2 == targetPosZ)
            )
            {
                GetComponent<UnitAnimator>().IsLongAttack = true;
                await Task.Delay(TimeSpan.FromSeconds(0.9f));
                GetComponent<UnitAnimator>().IsLongAttack = false;
            }
            else if (
                (attackerPosX + 1 == targetPosX && attackerPosZ + 1 == targetPosZ) ||
                (attackerPosX + 1 == targetPosX && attackerPosZ - 1 == targetPosZ) ||
                (attackerPosX - 1 == targetPosX && attackerPosZ + 1 == targetPosZ) ||
                (attackerPosX - 1 == targetPosX && attackerPosZ - 1 == targetPosZ)
            )
            {
                Debug.Log("MiddleAttack!!");
                GetComponent<UnitAnimator>().IsMiddleAttack = true;
                await Task.Delay(TimeSpan.FromSeconds(0.9f));
                GetComponent<UnitAnimator>().IsMiddleAttack = false;
            }
        }
        else
        {
            GetComponent<UnitAnimator>().IsAttack = true;
            await Task.Delay(TimeSpan.FromSeconds(0.9f));
            GetComponent<UnitAnimator>().IsAttack = false;
        }

        targetUnit.GetComponent<UnitAnimator>().IsDamaged = true;
        await Task.Delay(TimeSpan.FromSeconds(0.9f));
        targetUnit.GetComponent<UnitAnimator>().IsDamaged = false;

        int[,] playerTwoFogMapState =
            GameObject.Find("FogManager").GetComponent<FogManager>().GetPlayerTwoFogMapState();

        if (playerTwoFogMapState[
                GetComponent<UnitOwnIntPosition>().PosX,
                GetComponent<UnitOwnIntPosition>().PosZ
            ] == Fog.FOG_EXIST
        )
        {
            targetStatus.HitPoint -= attackerStatus.AttackPoint;
            Debug.Log("Attack damage: " + attackerStatus.AttackPoint);
            GameObject.Find("FogManager")
                .GetComponent<FogManager>()
                .SetPlayerTwoFogMapState(
                    GetComponent<UnitOwnIntPosition>().PosX,
                    GetComponent<UnitOwnIntPosition>().PosZ,
                    Fog.FOG_NOT_EXIST
                );
        }
        else
        {
            targetStatus.HitPoint -= (attackerStatus.AttackPoint - targetStatus.DefensPoint);
            Debug.Log("Attack damage: " + (attackerStatus.AttackPoint - targetStatus.DefensPoint));
        }

        targetStatus.SetUnitStatus(targetStatus);
    }

    public async void SurpriseAttack(GameObject targetUnit)
    {
        UnitStatus attackerStatus = GetComponent<UnitStatus>().GetUnitStatus();
        UnitStatus targetStatus = targetUnit.GetComponent<UnitStatus>().GetUnitStatus();

        GetComponent<UnitRotationController>().UnitRotation(
            targetUnit.GetComponent<UnitOwnIntPosition>().PosX - GetComponent<UnitOwnIntPosition>().PosX,
            targetUnit.GetComponent<UnitOwnIntPosition>().PosZ - GetComponent<UnitOwnIntPosition>().PosZ
        );

        targetUnit.GetComponent<UnitRotationController>().UnitRotation(
            GetComponent<UnitOwnIntPosition>().PosX - targetUnit.GetComponent<UnitOwnIntPosition>().PosX,
            GetComponent<UnitOwnIntPosition>().PosZ - targetUnit.GetComponent<UnitOwnIntPosition>().PosZ
        );

        targetUnit.GetComponent<UnitAnimator>().IsDamaged = true;
        await Task.Delay(TimeSpan.FromSeconds(0.9f));
        targetUnit.GetComponent<UnitAnimator>().IsDamaged = false;

        GameObject.Find("FogManager")
            .GetComponent<FogManager>()
            .SetPlayerOneFogMapState(
                GetComponent<UnitOwnIntPosition>().PosX,
                GetComponent<UnitOwnIntPosition>().PosZ,
                Fog.FOG_NOT_EXIST
            );

        GameObject.Find("FogManager")
            .GetComponent<FogManager>()
            .ClearFog(
                GetComponent<UnitOwnIntPosition>().PosX,
                GetComponent<UnitOwnIntPosition>().PosZ
            );

        GameObject.Find("FogManager")
            .GetComponent<FogManager>()
            .SetPlayerTwoFogMapState(
                targetUnit.GetComponent<UnitOwnIntPosition>().PosX,
                targetUnit.GetComponent<UnitOwnIntPosition>().PosZ,
                Fog.FOG_NOT_EXIST
            );

        targetStatus.HitPoint -= attackerStatus.AttackPoint;
        Debug.Log("Attacked damage: " + attackerStatus.AttackPoint);

        targetStatus.SetUnitStatus(targetStatus);
    }
}