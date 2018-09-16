using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asset.Scripts.Cards
{
    public class MoveCardController : ICardType
    {
        public bool SetCardTypePhase()
        {
            Transform player1Units = GameObject.Find("Player1Units").transform;
            switch (player1Units.childCount)
            {
                case 0:
                    Debug.Log("Don't exist MyUnit.");

                    return false;

                case 1:
                    foreach (Transform player1UnitChild in player1Units)
                    {
                        GameObject.Find("UnitMoveManager")
                            .GetComponent<UnitMoveManager>()
                            .SetMoveUnit(player1UnitChild.gameObject);
                    }

                    GameObject.Find("PhaseManager")
                        .GetComponent<PhaseManager>()
                        .SetNextPhase("SelectDestination");

                    Debug.Log("Phase: SelectDestination");

                    return true;

                default:
                    GameObject.Find("PhaseManager")
                        .GetComponent<PhaseManager>()
                        .SetNextPhase("SelectMoveUnit");

                    Debug.Log("Phase: SelectMoveUnit");

                    return true;
            }
        }
    }
}