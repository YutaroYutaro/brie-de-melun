using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asset.Scripts.MiniMap
{
    public class SelectMoveUnitPhase : IPhaseType
    {
        public void PhaseController(int posX, int posZ)
        {
            Transform player1UnitChildren = GameObject.Find("Player1Units").transform;

            foreach (Transform player1UnitChild in player1UnitChildren)
            {
                if (player1UnitChild.GetComponent<UnitOwnIntPosition>().PosX == posX &&
                    player1UnitChild.GetComponent<UnitOwnIntPosition>().PosZ == posZ)
                {
                    GameObject.Find("UnitMoveManager")
                        .GetComponent<UnitMoveManager>()
                        .SetMoveUnit(player1UnitChild.gameObject);

                    GameObject.Find("PhaseManager")
                        .GetComponent<PhaseManager>()
                        .SetNextPhase("SelectDestination");

                    Debug.Log("Phase: SelectDestination");

                    break;
                }
            }
        }
    }
}