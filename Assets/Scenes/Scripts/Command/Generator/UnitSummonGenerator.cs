using UnityEngine;
using SummonUnitTypeDefine;

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


        proximityUnit.GetComponent<Animator>().enabled = false;
        proximityUnit.GetComponent<Animator>().Play("Summon");
        proximityUnit.GetComponent<Animator>().enabled = true;

        proximityUnit.transform.SetParent(GameObject.Find("Player1Units").transform);
        proximityUnit.GetComponent<UnitOwnIntPosition>().PosX = posX;
        proximityUnit.GetComponent<UnitOwnIntPosition>().PosZ = posZ;

        int id = PhotonNetwork.AllocateViewID();
        proximityUnit.GetComponent<ViewId>().UnitViewId = id;

        EnemyUnitController.Instance.SummonUnit(
            new Vector3(4 - posX, 0.5f, 6 - posZ),
            Quaternion.identity,
            id,
            SummonUnitTypeDefine.SummonUnitType.PROXIMITY
        );
    }

    public void SummonRemoteAttackUnit(int posX, int posZ)
    {
        GameObject remoteAttackUnit =
            Instantiate(
                RemoteAttackPrefab,
                new Vector3(posX, 0.5f, posZ),
                Quaternion.identity
            );

        remoteAttackUnit.GetComponent<Animator>().enabled = false;
        remoteAttackUnit.GetComponent<Animator>().Play("Summon");
        remoteAttackUnit.GetComponent<Animator>().enabled = true;

        remoteAttackUnit.transform.SetParent(GameObject.Find("Player1Units").transform);
        remoteAttackUnit.GetComponent<UnitOwnIntPosition>().PosX = posX;
        remoteAttackUnit.GetComponent<UnitOwnIntPosition>().PosZ = posZ;

        int id = PhotonNetwork.AllocateViewID();
        remoteAttackUnit.GetComponent<ViewId>().UnitViewId = id;

        EnemyUnitController.Instance.SummonUnit(
            new Vector3(4 - posX, 0.5f, 6 - posZ),
            Quaternion.identity,
            id,
            SummonUnitTypeDefine.SummonUnitType.REMOTE
        );
    }

    public void SummonReconnaissanceUnit(int posX, int posZ)
    {
        GameObject reconnaissanecUnit =
            Instantiate(
                ReconnaissanecPrefab,
                new Vector3(posX, 1.5f, posZ),
                Quaternion.identity
            );

        reconnaissanecUnit.GetComponent<Animator>().enabled = false;
        reconnaissanecUnit.GetComponent<Animator>().Play("Summon");
        reconnaissanecUnit.GetComponent<Animator>().enabled = true;

        reconnaissanecUnit.transform.SetParent(GameObject.Find("Player1Units").transform);
        reconnaissanecUnit.GetComponent<UnitOwnIntPosition>().PosX = posX;
        reconnaissanecUnit.GetComponent<UnitOwnIntPosition>().PosZ = posZ;

        int id = PhotonNetwork.AllocateViewID();
        reconnaissanecUnit.GetComponent<ViewId>().UnitViewId = id;

        EnemyUnitController.Instance.SummonUnit(
            new Vector3(4 - posX, 1.5f, 6 - posZ),
            Quaternion.identity,
            id,
            SummonUnitTypeDefine.SummonUnitType.RECONNAISSANCE
        );
    }

    public int SummonUnitType { get; set; }
}