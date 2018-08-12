using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class UnitSummonGenerator : MonoBehaviour
{
    public GameObject ProximityAttackPrefab;
    public GameObject RemoteAttackPrefab;
    public GameObject ReconnaissanecPrefab;

    public int _summonUnitType;

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

    public void SummonReconnaissanceUnit(int posX, int posZ)
    {
        GameObject reconnaissanecUnit = Instantiate(ReconnaissanecPrefab);

        reconnaissanecUnit.transform.SetParent(GameObject.Find("Player1Units").transform);

        GameObject.Find("UnitManager").GetComponent<UnitManager>().GetMyUnitList().Add(reconnaissanecUnit);

        reconnaissanecUnit.GetComponent<UnitOwnIntPosition>().PosX = posX;
        reconnaissanecUnit.GetComponent<UnitOwnIntPosition>().PosZ = posZ;
    }

    public int SummonUnitType
    {
        get { return _summonUnitType; }
        set { _summonUnitType = value; }
    }
}