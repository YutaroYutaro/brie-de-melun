using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Asset.Scripts.MiniMap
{
    public class SelectAttackerUnitPhase : IPhaseType
    {
        public void PhaseController(int posX, int posZ)
        {
            UnitAttackManager unitAttackManager =
                GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>();

            List<UnitAttackManager.AttackerAndTarget> attackerAndTargetList =
                unitAttackManager.GetAttackerAndTargetList();

            foreach (UnitAttackManager.AttackerAndTarget attackerAndTarget in attackerAndTargetList)
            {
                if (attackerAndTarget.Attacker.GetComponent<UnitOwnIntPosition>().PosX == posX &&
                    attackerAndTarget.Attacker.GetComponent<UnitOwnIntPosition>().PosZ == posZ)
                {
                    Debug.Log("Debug: Set Attacker Unit");

                    if (attackerAndTarget.Target.Count == 1)
                    {
                        unitAttackManager.SelectedAttacker =
                            unitAttackManager.GetAttackerAndTargetList().First().Attacker;

                        unitAttackManager.MiniMapUnitAttack(attackerAndTarget.Target[0]);

                        GameObject.Find("PhaseManager")
                            .GetComponent<PhaseManager>()
                            .SetNextPhase("SelectUseCard");

                        Debug.Log("Phase: SelectUseCard");
                    }
                    else
                    {
                        unitAttackManager.SetSelectedAttackerAndTargetUnit(attackerAndTarget);
                        GameObject.Find("PhaseManager")
                            .GetComponent<PhaseManager>()
                            .SetNextPhase("SelectAttackTargetUnit");

                        Debug.Log("Phase: SelectAttackTargetUnit");
                    }

                    break;
                }
            }
        }
    }
}