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
        photonView.RPC("EnemyUnitMove", PhotonTargets.Others, id, posX, posY, posZ);
    }

    [PunRPC]
    public void EnemyUnitMove(int id, int posX, float posY, int posZ)
    {
        Vector3 nextDestination = new Vector3(4 - posX, posY, 6 - posZ);
        PhotonView unitGameobject = PhotonView.Get(PhotonView.Find(id));

        unitGameobject.GetComponent<UnitRotationController>().UnitRotation(
            4 - posX - unitGameobject.GetComponent<UnitOwnIntPosition>().PosX,
            6 - posZ - unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ
        );

        Debug.Log(4 - posX - unitGameobject.GetComponent<UnitOwnIntPosition>().PosX);
        Debug.Log(6 - posZ - unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ);

        unitGameobject.GetComponent<UnitOwnIntPosition>().PosX = 4 - posX;
        unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ = 6 - posZ;

        // ToDo: アクティブ状態になった時のアニメーション遷移
        Transform foggyMapObjectsChildren = GameObject.Find("FoggyMapObjects").transform;

        foreach (Transform foggyMapObjectsChild in foggyMapObjectsChildren)
        {
            if (
                unitGameobject.GetComponent<UnitOwnIntPosition>().PosX
                == Mathf.RoundToInt(foggyMapObjectsChild.position.x) &&
                unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ
                == Mathf.RoundToInt(foggyMapObjectsChild.position.z)
            )
            {
                unitGameobject.gameObject.SetActive(false);
            }
        }

        Transform clearMapObjectsChildren = GameObject.Find("ClearMapObjects").transform;

        foreach (Transform clearMapObjectsChild in clearMapObjectsChildren)
        {
            if (
                unitGameobject.GetComponent<UnitOwnIntPosition>().PosX
                == Mathf.RoundToInt(clearMapObjectsChild.position.x) &&
                unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ
                == Mathf.RoundToInt(clearMapObjectsChild.position.z)
            )
            {
                unitGameobject.gameObject.SetActive(true);
            }
        }

        unitGameobject.transform.DOMove(nextDestination, 1.3f);
    }

    public void UnitAttack(int id, int targetPosX, int targetPosZ)
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC("EnemyUnitAttack", PhotonTargets.Others, id, targetPosX, targetPosZ);
    }

    [PunRPC]
    public async void EnemyUnitAttack(int id, int targetPosX, int targetPosZ)
    {
        PhotonView unitGameobject = PhotonView.Get(PhotonView.Find(id));
        Transform player1UnitsChildren = GameObject.Find("Player1Units").transform;

        foreach (Transform player1UnitsChild in player1UnitsChildren)
        {
            if
            (
                targetPosX == player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosX &&
                targetPosZ == player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosZ
            )
            {
                unitGameobject.GetComponent<UnitAnimator>().IsAttack = true;
                await Task.Delay(TimeSpan.FromSeconds(0.9f));
                unitGameobject.GetComponent<UnitAnimator>().IsAttack = false;

                player1UnitsChild.GetComponent<UnitAnimator>().IsDamaged = true;
                await Task.Delay(TimeSpan.FromSeconds(0.9f));
                player1UnitsChild.GetComponent<UnitAnimator>().IsDamaged = false;
                unitGameobject.GetComponent<UnitAttack>().MiniMapClickUnitAttack(player1UnitsChild.gameObject);
                break;
            }
        }
    }

    public void SummonUnit(Vector3 pos, Quaternion rot, int id)
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC(
            "SummonEnemyProximityAttackUnit",
            PhotonTargets.Others,
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

        Transform foggyMapObjectsChildren = GameObject.Find("FoggyMapObjects").transform;

        foreach (Transform foggyMapObjectsChild in foggyMapObjectsChildren)
        {
            if (
                newPlayer.GetComponent<UnitOwnIntPosition>().PosX
                == Mathf.RoundToInt(foggyMapObjectsChild.position.x) &&
                newPlayer.GetComponent<UnitOwnIntPosition>().PosZ
                == Mathf.RoundToInt(foggyMapObjectsChild.position.z)
            )
            {
                newPlayer.gameObject.SetActive(false);
            }
        }

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