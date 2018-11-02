using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Async;
using DG.Tweening;
using System.Threading.Tasks;
using System;
using FogDefine;
using SummonUnitTypeDefine;
using UnityEditor;
using UnityEngine.UI;

public class EnemyUnitController : SingletonMonoBehaviour<EnemyUnitController>
{
    [SerializeField] private GameObject _proximityAttackPrefab;
    [SerializeField] private GameObject _remoteAttackPrefab;
    [SerializeField] private GameObject _reconnaissanecPrefab;
    [SerializeField] private Text _loseText;

    public void TurnStart()
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC("EnemyTurnStart", PhotonTargets.Others);
    }

    [PunRPC]
    public void EnemyTurnStart()
    {
        PhaseManager.Instance.SetNextPhase("SelectUseCard");
    }

    public void TurnEnd()
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC("EnemyTurnEnd", PhotonTargets.Others);
    }

    [PunRPC]
    public void EnemyTurnEnd()
    {
        PhaseManager.Instance.SetNextPhase("EnemyTurn");
    }

    public void TowerBreak(int id)
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC("EnemyTowerBreak", PhotonTargets.Others, id);
    }

    [PunRPC]
    public async void EnemyTowerBreak(int id)
    {
        PhotonView unitGameobject = PhotonView.Get(PhotonView.Find(id));
        Vector3 nextDestination = new Vector3(2, unitGameobject.transform.position.y, -1);

        unitGameobject.GetComponent<UnitAnimator>().IsMove = true;
        unitGameobject.transform.DOMove(nextDestination, 1.3f);
        await Task.Delay(TimeSpan.FromSeconds(1.4f));
        unitGameobject.GetComponent<UnitAnimator>().IsMove = false;

        unitGameobject.GetComponent<UnitAnimator>().IsAttack = true;
        await Task.Delay(TimeSpan.FromSeconds(0.9f));
        unitGameobject.GetComponent<UnitAnimator>().IsAttack = false;

        MyTowerModel.Instance.TowerHitPointReactiveProperty.Value -= 1;

        unitGameobject.GetComponent<UnitAnimator>().IsDefeated = true;
        await Task.Delay(TimeSpan.FromSeconds(1.4f));
        Destroy(unitGameobject.gameObject);
    }

    public void GameEndRpc()
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC("EnemyGameEnd", PhotonTargets.Others);
    }

    [PunRPC]
    public void EnemyGameEnd()
    {
        Debug.Log("You Lose!");
        _loseText.GetComponent<Text>().enabled = true;
        StartCoroutine(GameEndEnumerator());
    }

    IEnumerator GameEndEnumerator()
    {
        // Todo: ゲーム終了時の処理をゲームリセットかゲーム終了か
        while (!Input.anyKeyDown) yield return null;

//        PhotonNetwork.Disconnect();
//        SceneManager.LoadScene("SampleScene");

        EditorApplication.isPlaying = false;

//      Application.Quit();
    }

    public void UnitMove(int id, int posX, float posY, int posZ)
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC("EnemyUnitMove", PhotonTargets.Others, id, posX, posY, posZ);
    }

    [PunRPC]
    public async void EnemyUnitMove(int id, int posX, float posY, int posZ)
    {
        Vector3 nextDestination = new Vector3(4 - posX, posY, 6 - posZ);
        PhotonView unitGameobject = PhotonView.Get(PhotonView.Find(id));

        FogManager
            .Instance
            .SetPlayerTwoFogMapState
            (
                4 - posX,
                6 - posZ,
                Fog.FOG_NOT_EXIST
            );

        unitGameobject
            .GetComponent<UnitRotationController>()
            .UnitRotation
            (
                4 - posX - unitGameobject.GetComponent<UnitOwnIntPosition>().PosX,
                6 - posZ - unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ
            );

        unitGameobject.GetComponent<UnitOwnIntPosition>().SetUnitOwnIntPosition(4 - posX, 6 - posZ);

        // ToDo: アクティブ状態になった時のアニメーション遷移
        Transform foggyMapObjectsChildren = GameObject.Find("FoggyMapObjects").transform;

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
                break;
            }
        }


        unitGameobject.GetComponent<Animator>().enabled = false;
        unitGameobject.GetComponent<Animator>().Play("Move");
        unitGameobject.GetComponent<Animator>().enabled = true;
//        unitGameobject.GetComponent<UnitAnimator>().IsMove = true;
        unitGameobject.transform.DOMove(nextDestination, 1.3f);
        await Task.Delay(TimeSpan.FromSeconds(1.4f));
//        unitGameobject.GetComponent<UnitAnimator>().IsMove = false;

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
                break;
            }
        }
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
        Transform foggyMapObjectsChildren = GameObject.Find("FoggyMapObjects").transform;
        Transform player1UnitsChildren = GameObject.Find("Player1Units").transform;
        bool isSurpriseAttack = false;

        foreach (Transform foggyMapObjectsChild in foggyMapObjectsChildren)
        {
            if (
                unitGameobject.GetComponent<UnitOwnIntPosition>().PosX
                == Mathf.RoundToInt(foggyMapObjectsChild.position.x) &&
                unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ
                == Mathf.RoundToInt(foggyMapObjectsChild.position.z)
            )
            {
                unitGameobject.gameObject.SetActive(true);
                FogManager
                    .Instance
                    .ClearFog
                    (
                        unitGameobject.GetComponent<UnitOwnIntPosition>().PosX,
                        unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ
                    );
                isSurpriseAttack = true;
                break;
            }
        }

        foreach (Transform player1UnitsChild in player1UnitsChildren)
        {
            if
            (
                targetPosX == (4 - player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosX) &&
                targetPosZ == (6 - player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosZ)
            )
            {
                unitGameobject.GetComponent<UnitRotationController>().UnitRotation(
                    player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosX - unitGameobject.GetComponent<UnitOwnIntPosition>().PosX,
                    player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosZ - unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ
                );

                player1UnitsChild.GetComponent<UnitRotationController>().UnitRotation(
                    unitGameobject.GetComponent<UnitOwnIntPosition>().PosX - player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosX,
                    unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ - player1UnitsChild.GetComponent<UnitOwnIntPosition>().PosZ
                );

                unitGameobject.GetComponent<UnitAnimator>().IsAttack = true;
                await Task.Delay(TimeSpan.FromSeconds(0.9f));
                unitGameobject.GetComponent<UnitAnimator>().IsAttack = false;

                player1UnitsChild.GetComponent<UnitAnimator>().IsDamaged = true;
                await Task.Delay(TimeSpan.FromSeconds(0.9f));
                player1UnitsChild.GetComponent<UnitAnimator>().IsDamaged = false;

                if (isSurpriseAttack)
                {
                    player1UnitsChild.GetComponent<UnitStatus>().GetUnitStatus().HitPoint
                        -= unitGameobject.GetComponent<UnitStatus>().GetUnitStatus().AttackPoint;
                }
                else
                {
                    player1UnitsChild.GetComponent<UnitStatus>().GetUnitStatus().HitPoint
                        -= unitGameobject.GetComponent<UnitStatus>().GetUnitStatus().AttackPoint
                           - player1UnitsChild.GetComponent<UnitStatus>().GetUnitStatus().DefensPoint;
                }

                break;
            }
        }
    }

    public void UnitReconnaissance(int id)
    {
        PhotonView photonView = GetComponent<PhotonView>();
        photonView.RPC(
            "EnemyUnitReconnaissance",
            PhotonTargets.Others,
            id
        );
    }

    [PunRPC]
    public async void EnemyUnitReconnaissance(int id)
    {
        PhotonView unitGameobject = PhotonView.Get(PhotonView.Find(id));
        int unitPosX = unitGameobject.GetComponent<UnitOwnIntPosition>().PosX;
        int unitPosZ = unitGameobject.GetComponent<UnitOwnIntPosition>().PosZ;

        unitGameobject.GetComponent<UnitAnimator>().IsSearch = true;
        await Task.Delay(TimeSpan.FromSeconds(1.4f));
        unitGameobject.GetComponent<UnitAnimator>().IsSearch = false;

        if (unitPosX < 4)
        {
            FogManager.Instance.SetPlayerTwoFogMapState(unitPosX + 1, unitPosZ, Fog.FOG_NOT_EXIST);
        }

        if (unitPosX > 0)
        {
            FogManager.Instance.SetPlayerTwoFogMapState(unitPosX - 1, unitPosZ, Fog.FOG_NOT_EXIST);
        }

        if (unitPosZ < 6)
        {
            FogManager.Instance.SetPlayerTwoFogMapState(unitPosX, unitPosZ + 1, Fog.FOG_NOT_EXIST);
        }

        if (unitPosZ > 0)
        {
            FogManager.Instance.SetPlayerTwoFogMapState(unitPosX, unitPosZ - 1, Fog.FOG_NOT_EXIST);
        }

        int[,] a = FogManager.Instance.PlayerTwoFogMapState;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                Debug.Log("{" + i + ", " + j + "}: " + a[i, j]);
            }
        }
    }


    public void SummonUnit(Vector3 pos, Quaternion rot, int id, int unitType)
    {
        PhotonView photonView = GetComponent<PhotonView>();

        switch (unitType)
        {
            case SummonUnitTypeDefine.SummonUnitType.PROXIMITY:
                photonView.RPC(
                    "SummonEnemyProximityAttackUnit",
                    PhotonTargets.Others,
                    pos,
                    rot,
                    id
                );
                break;
            case SummonUnitTypeDefine.SummonUnitType.REMOTE:
                photonView.RPC(
                    "SummonEnemyRemoteAttackUnit",
                    PhotonTargets.Others,
                    pos,
                    rot,
                    id
                );
                break;
            case SummonUnitTypeDefine.SummonUnitType.RECONNAISSANCE:
                photonView.RPC(
                    "SummonEnemyReconnaissanceUnit",
                    PhotonTargets.Others,
                    pos,
                    rot,
                    id
                );
                break;
        }
    }

    [PunRPC]
    public void SummonEnemyProximityAttackUnit(Vector3 pos, Quaternion rot, int id)
    {
        GameObject newPlayer = Instantiate(_proximityAttackPrefab, pos, rot);
        newPlayer.transform.eulerAngles = new Vector3(0, 180f, 0);
        newPlayer.GetComponent<UnitOwnIntPosition>().SetUnitOwnIntPosition((int) pos.x, (int) pos.z);

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
    }

    [PunRPC]
    public void SummonEnemyRemoteAttackUnit(Vector3 pos, Quaternion rot, int id)
    {
        GameObject newPlayer = Instantiate(_remoteAttackPrefab, pos, rot);
        newPlayer.transform.eulerAngles = new Vector3(0, 180f, 0);
        newPlayer.GetComponent<UnitOwnIntPosition>().SetUnitOwnIntPosition((int) pos.x, (int) pos.z);

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
    }

    [PunRPC]
    public void SummonEnemyReconnaissanceUnit(Vector3 pos, Quaternion rot, int id)
    {
        GameObject newPlayer = Instantiate(_reconnaissanecPrefab, pos, rot);
        newPlayer.transform.eulerAngles = new Vector3(0, 180f, 0);
        newPlayer.GetComponent<UnitOwnIntPosition>().SetUnitOwnIntPosition((int) pos.x, (int) pos.z);

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
    }
}