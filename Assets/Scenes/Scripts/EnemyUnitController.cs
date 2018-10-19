using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Async;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public class EnemyUnitController : SingletonMonoBehaviour<EnemyUnitController>
{
    [SerializeField] private GameObject _proximityAttackPrefab;
    [SerializeField] private GameObject _remoteAttackPrefab;
    [SerializeField] private GameObject _reconnaissanecPrefab;

    public void UnitMove(int id, int posX, float posY, int posZ)
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC("EnemyUnitMove", PhotonTargets.All, id, posX, posY, posZ);
    }

    [PunRPC]
    public void EnemyUnitMove(int id, int posX, float posY, int posZ)
    {
        PhotonView unit = PhotonView.Find(id);
        Vector3 nextDestination = new Vector3(4 - posX, posY, 6 - posZ);
        PhotonView unitGameobject = PhotonView.Get(unit);

        unitGameobject.GetComponent<UnitRotationController>().UnitRotation(
            4 - posX - unitGameobject.GetComponent<UnitOwnIntPosition>().PosX,
            6 - posZ - unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ
        );

        Debug.Log(4 - posX - unitGameobject.GetComponent<UnitOwnIntPosition>().PosX);
        Debug.Log(6 - posZ - unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ);

        unitGameobject.GetComponent<UnitOwnIntPosition>().PosX = 4 - posX;
        unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ = 6 - posZ;

        unit.transform.DOMove(nextDestination, 1.3f);
    }

    public void SummonUnit(Vector3 pos, Quaternion rot, int id)
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC(
            "SummonEnemyProximityAttackUnit",
            PhotonTargets.All,
            pos,
            rot,
            id
        );
    }

    [PunRPC]
    public void SummonEnemyProximityAttackUnit(Vector3 pos, Quaternion rot, int id)
    {
        GameObject newPlayer = Instantiate(_proximityAttackPrefab, pos, rot);
        newPlayer.transform.eulerAngles = new Vector3(0, 180f, 0);
        newPlayer.GetComponent<UnitOwnIntPosition>().PosX = (int) pos.x;
        newPlayer.GetComponent<UnitOwnIntPosition>().PosZ = (int) pos.z;

        // Set player's PhotonView
        PhotonView[] nViews = newPlayer.GetComponents<PhotonView>();
        nViews[0].viewID = id;

        newPlayer.transform.SetParent(GameObject.Find("Player2Units").transform);

        Debug.Log(nViews);
        Debug.Log(nViews[0].viewID);
    }

    [PunRPC]
    public void SummonEnemyRemoteAttackUnit(Vector3 pos, Quaternion rot, int id)
    {
        GameObject newPlayer = Instantiate(_remoteAttackPrefab, pos, rot);

        // Set player's PhotonView
        PhotonView[] nViews = newPlayer.GetComponentsInChildren<PhotonView>();
        nViews[nViews.Length].viewID = id;
    }

    [PunRPC]
    public void SummonEnemyReconnaissanceUnit(Vector3 pos, Quaternion rot, int id)
    {
        GameObject newPlayer = Instantiate(_reconnaissanecPrefab, pos, rot);

        // Set player's PhotonView
        PhotonView[] nViews = newPlayer.GetComponentsInChildren<PhotonView>();
        nViews[nViews.Length].viewID = id;
    }
}