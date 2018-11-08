using UnityEngine;
using System.Collections.Generic;

namespace Asset.Scripts.MiniMap
{
    public class SelectAttackTargetUnitPhase : IPhaseType
    {
        public void PhaseController(int posX, int posZ)
        {
            UnitAttackManager unitAttackManager =
                GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>();

            List<GameObject> selectedTargetList =
                unitAttackManager.GetSelectedAttackerAndTargetUnit().Target;

            Debug.Log("Null: " + selectedTargetList);

            foreach (GameObject selectedTarget in selectedTargetList)
            {
                if (selectedTarget.GetComponent<UnitOwnIntPosition>().PosX == posX &&
                    selectedTarget.GetComponent<UnitOwnIntPosition>().PosZ == posZ)
                {
                    unitAttackManager.MiniMapUnitAttack(selectedTarget);

                    GameObject.Find("PhaseManager")
                        .GetComponent<PhaseManager>()
                        .SetNextPhase("SelectUseCard");

                    Debug.Log("Phase: SelectUseCard");
                }
            }
        }
    }
}