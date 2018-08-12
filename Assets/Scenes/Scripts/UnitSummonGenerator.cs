using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSummonGenerator : MonoBehaviour
{
    public GameObject ProximityAttackPrefab;
    public GameObject RemoteAttackPrefab;
    public GameObject SummonReconnaissanecPrefab;

    public void SummonProximityAttackUnit(int posX, int posZ)
    {
        GameObject proximityUnit = Instantiate(ProximityAttackPrefab, new Vector3(posX, 1 , posZ), Quaternion.identity);

        proximityUnit.transform.SetParent(GameObject.Find("Player1Units").transform);

        GameObject.Find("UnitManager").GetComponent<UnitManager>().GetMyUnitList().Add(proximityUnit);

        proximityUnit.GetComponent<UnitOwnIntPosition>().PosX = posX;
        proximityUnit.GetComponent<UnitOwnIntPosition>().PosZ = posZ;
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