using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asset.Scripts.Cards
{
    public class ReconnaissanceCardController : ICardType
    {
        public bool SetCardTypePhase()
        {
            int numberOfReconnaissanceUnit = 0;
            GameObject aloneReconnaissanceUnit = new GameObject();
            foreach (Transform unit in GameObject.Find("Player1Units").transform)
            {
                if (unit.gameObject.CompareTag("ReconnaissanceUnit"))
                {
                    ++numberOfReconnaissanceUnit;
                    aloneReconnaissanceUnit = unit.gameObject;
                }
            }

            switch (numberOfReconnaissanceUnit)
            {
                case 0:
                    Debug.Log("Don't exist ReconnaissanceUnit.");

                    return false;

                case 1:
                    Debug.Log("This is a ReconnaissanceCard");

                    aloneReconnaissanceUnit
                        .GetComponent<UnitReconnaissanceController>()
                        .UnitReconnaissance();

                    GameObject.Find("PhaseManager")
                        .GetComponent<PhaseManager>()
                        .SetNextPhase("SelectUseCard");

                    Debug.Log("Phase: SelectUseCard");

                    return true;

                default:
                    GameObject.Find("PhaseManager")
                        .GetComponent<PhaseManager>()
                        .SetNextPhase("SelectReconnaissanceUnit");

                    Debug.Log("Phase: SelectReconnaissanceUnit");

                    return true;
            }
        }
    }
}