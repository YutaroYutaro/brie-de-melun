using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class BreakTowerController : MonoBehaviour
{
    public async void BreakTower(int posX, float posY, int posZ)
    {
        if (posX == 2 && posZ == 6 && CompareTag("ProximityAttackUnit"))
        {
            Vector3 nextDestination = new Vector3(2, posY, 7);

            GetComponent<UnitAnimator>().IsMove = true;
            transform.DOMove(nextDestination, 1.3f);
            await Task.Delay(TimeSpan.FromSeconds(1.4f));
            GetComponent<UnitAnimator>().IsMove = false;

            GetComponent<UnitAnimator>().IsAttack = true;
            await Task.Delay(TimeSpan.FromSeconds(0.9f));
            GetComponent<UnitAnimator>().IsAttack = false;

            GetComponent<UnitAnimator>().IsDefeated = true;
            await Task.Delay(TimeSpan.FromSeconds(1.4f));
            Destroy(gameObject);

            TowerModel.Instance.TowerHitPointReactiveProperty.Value -= 1;

            GameObject.Find("PhaseManager")
                .GetComponent<PhaseManager>()
                .SetNextPhase("SelectUseCard");
        }
    }
}