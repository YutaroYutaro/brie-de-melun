using UnityEngine;

public class UnitSummonGenerator : MonoBehaviour
{
    public GameObject ProximityAttackPrefab;
    public GameObject RemoteAttackPrefab;
    public GameObject ReconnaissanecPrefab;

    private int _summonUnitType;

    public void SummonProximityAttackUnit(int posX, int posZ)
    {
        GameObject proximityUnit =
            Instantiate(
                ProximityAttackPrefab,
                new Vector3(posX, 0.5f, posZ),
                Quaternion.identity
            );

        proximityUnit.transform.SetParent(GameObject.Find("Player1Units").transform);
        proximityUnit.GetComponent<UnitOwnIntPosition>().PosX = posX;
        proximityUnit.GetComponent<UnitOwnIntPosition>().PosZ = posZ;
    }

    public void SummonRemoteAttackUnit(int posX, int posZ)
    {
        GameObject remoteAttackUnit =
            Instantiate(
                RemoteAttackPrefab,
                new Vector3(posX, 1, posZ),
                Quaternion.identity
            );

        remoteAttackUnit.transform.SetParent(GameObject.Find("Player1Units").transform);
        remoteAttackUnit.GetComponent<UnitOwnIntPosition>().PosX = posX;
        remoteAttackUnit.GetComponent<UnitOwnIntPosition>().PosZ = posZ;
    }

    public void SummonReconnaissanceUnit(int posX, int posZ)
    {
        GameObject reconnaissanecUnit =
            Instantiate(
                ReconnaissanecPrefab,
                new Vector3(posX, 1.5f, posZ),
                Quaternion.identity
            );

        reconnaissanecUnit.transform.SetParent(GameObject.Find("Player1Units").transform);
        reconnaissanecUnit.GetComponent<UnitOwnIntPosition>().PosX = posX;
        reconnaissanecUnit.GetComponent<UnitOwnIntPosition>().PosZ = posZ;
    }

    public int SummonUnitType { get; set; }
}