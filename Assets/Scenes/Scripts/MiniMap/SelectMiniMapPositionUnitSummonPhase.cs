using UnityEngine;

namespace Asset.Scripts.MiniMap
{
    public class SelectMiniMapPositionUnitSummonPhase : IPhaseType
    {
        public void PhaseController(int posX, int posZ)
        {
            if (posZ == 0 && (posX == 1 || posX == 2 || posX == 3))
                {
                    if (GameObject.Find("Player1Units").transform.childCount != 0)
                    {
                        Transform player1UnitChildren = GameObject.Find("Player1Units").transform;

                        foreach (Transform player1UnitChild in player1UnitChildren)
                        {
                            if (player1UnitChild.GetComponent<UnitOwnIntPosition>().PosX == posX &&
                                player1UnitChild.GetComponent<UnitOwnIntPosition>().PosZ == posZ)
                            {
                                Debug.Log("Exist Unit");
                                return;
                            }
                        }
                    }

                    switch (GameObject.Find("UnitSummonGenerator")
                        .GetComponent<UnitSummonGenerator>()
                        .SummonUnitType
                    )
                    {
                        case SummonUnitTypeDefine.SummonUnitType.PROXIMITY:
                            GameObject.Find("UnitSummonGenerator")
                                .GetComponent<UnitSummonGenerator>()
                                .SummonProximityAttackUnit(
                                    posX,
                                    posZ
                                );

                            break;

                        case SummonUnitTypeDefine.SummonUnitType.REMOTE:
                            GameObject.Find("UnitSummonGenerator")
                                .GetComponent<UnitSummonGenerator>()
                                .SummonRemoteAttackUnit(
                                    posX,
                                    posZ
                                );

                            break;

                        case SummonUnitTypeDefine.SummonUnitType.RECONNAISSANCE:
                            GameObject.Find("UnitSummonGenerator")
                                .GetComponent<UnitSummonGenerator>()
                                .SummonReconnaissanceUnit(
                                    posX,
                                    posZ
                                );

                            break;
                    }

                    GameObject.Find("PhaseManager")
                        .GetComponent<PhaseManager>()
                        .SetNextPhase("SelectUseCard");

                    Debug.Log("Phase: SelectUseCard");
                }

        }
    }
}