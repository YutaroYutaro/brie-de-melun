using UnityEngine;

namespace Asset.Scripts.MiniMap
{
    public class SelectReconnaissanceUnitPhase : IPhaseType
    {
        public void PhaseController(int posX, int posZ)
        {
            foreach (Transform unit in GameObject.Find("Player1Units").transform)
            {
                if (unit.gameObject.CompareTag("ReconnaissanceUnit") &&
                    posX == unit.GetComponent<UnitOwnIntPosition>().PosX &&
                    posZ == unit.GetComponent<UnitOwnIntPosition>().PosZ
                )
                {
                    unit.GetComponent<UnitReconnaissanceController>()
                        .UnitReconnaissance();

                    GameObject.Find("PhaseManager")
                        .GetComponent<PhaseManager>()
                        .SetNextPhase("SelectUseCard");

                    Debug.Log("Phase: SelectUseCard");
                }
            }
        }
    }
}