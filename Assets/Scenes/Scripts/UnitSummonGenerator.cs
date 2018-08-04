using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSummonGenerator : MonoBehaviour
{
    public GameObject ProximityAttackPrefab;
    public GameObject RemoteAttackPrefab;
    public GameObject SummonReconnaissanecPrefab;

    public void SummonProximityAttackUnit()
    {
        GameObject proximityUnit = Instantiate(ProximityAttackPrefab);

        proximityUnit.transform.SetParent(GameObject.Find("Player1Units").transform);
    }

    public void SummonRemoteAttackUnit()
    {
        GameObject proximityUnit = Instantiate(RemoteAttackPrefab);

        proximityUnit.transform.SetParent(GameObject.Find("Player1Units").transform);
    }

    public void SummonReconnaissanceUnit()
    {
        GameObject proximityUnit = Instantiate(SummonReconnaissanecPrefab);

        proximityUnit.transform.SetParent(GameObject.Find("Player1Units").transform);
    }
}