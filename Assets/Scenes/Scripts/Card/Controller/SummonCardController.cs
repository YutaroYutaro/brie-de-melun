using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asset.Scripts.Cards
{
    public class SummonCardController : ICardType
    {
        public bool SetCardTypePhase()
        {
            if (GameObject.Find("Player1Units").transform.childCount != 0)
            {
                Transform player1UnitChildren = GameObject.Find("Player1Units").transform;
                bool existUnit1 = false;
                bool existUnit2 = false;
                bool existUnit3 = false;

                foreach (Transform player1UnitChild in player1UnitChildren)
                {
                    if (Mathf.RoundToInt(player1UnitChild.position.x) == 1 &&
                        Mathf.RoundToInt(player1UnitChild.position.z) == 0)
                    {
                        Debug.Log("Exist Unit (1, 1, 0)");

                        existUnit1 = true;
                    }
                    else if (Mathf.RoundToInt(player1UnitChild.position.x) == 2 &&
                             Mathf.RoundToInt(player1UnitChild.position.z) == 0)
                    {
                        Debug.Log("Exist Unit (2, 1, 0)");

                        existUnit2 = true;
                    }
                    else if (Mathf.RoundToInt(player1UnitChild.position.x) == 3 &&
                             Mathf.RoundToInt(player1UnitChild.position.z) == 0)
                    {
                        Debug.Log("Exist Unit (3, 1, 0)");

                        existUnit3 = true;
                    }

                    if (existUnit1 && existUnit2 && existUnit3)
                    {
                        Debug.Log("Can't Summon");

                        return false;
                    }
                }
            }

            Debug.Log("This is a SummonCard");

            GameObject.Find("PhaseManager")
                .GetComponent<PhaseManager>()
                .SetNextPhase("SelectMiniMapPositionUnitSummon");

            Debug.Log("Phase: SelectMiniMapPositionUnitSummon");
            return true;
        }
    }
}