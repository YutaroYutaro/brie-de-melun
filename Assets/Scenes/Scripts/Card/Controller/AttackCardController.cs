using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Asset.Scripts.Cards
{
    public class AttackCardController : ICardType
    {
        public bool SetCardTypePhase()
        {
            UnitAttackManager unitAttackManager =
                GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>();

            //攻撃できるユニットが存在するか判定
            if (!(unitAttackManager.ExistAttackTargetUnit()))
            {
                Debug.Log("Don't exist Attacker or Target.");

                return false;
            }

            //アタッカーユニットが1体か判定
            if (unitAttackManager.GetAttackerAndTargetList().Count != 1)
            {
                GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                    .SetNextPhase("SelectAttackerUnit");

                Debug.Log("Phase: SelectAttackerUnit");

                return true;
            }

            //ターゲットユニットが1体か判定
            if (unitAttackManager
                    .GetAttackerAndTargetList().First().Target.Count != 1)
            {
                unitAttackManager.SelectedAttacker =
                    unitAttackManager.GetAttackerAndTargetList().First().Attacker;

                unitAttackManager.SetSelectedAttackerAndTargetUnit(unitAttackManager
                    .GetAttackerAndTargetList().First());

                GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                    .SetNextPhase("SelectAttackTargetUnit");

                Debug.Log("Phase: SelectAttackTargetUnit");

                return true;
            }

            unitAttackManager.SelectedAttacker =
                unitAttackManager.GetAttackerAndTargetList().First().Attacker;

            unitAttackManager.MiniMapUnitAttack(
                unitAttackManager.GetAttackerAndTargetList().First().Target.First()
            );

            GameObject.Find("PhaseManager").GetComponent<PhaseManager>()
                .SetNextPhase("SelectUseCard");

            Debug.Log("Phase: SelectUseCard");

            return true;
        }
    }
}