using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network : MonoBehaviour
{
    private RoomOptions _roomOptions;

    void Start()
    {
        // Photonに接続する(引数でゲームのバージョンを指定できる)
        PhotonNetwork.ConnectUsingSettings(null);
        _roomOptions = new RoomOptions()
        {
            MaxPlayers = 2,
            IsOpen = true,
            IsVisible = true
        };
    }

    // ロビーに入ると呼ばれる
    void OnJoinedLobby()
    {
        Debug.Log("ロビーに入りました。");

        // ルームに入室する
        PhotonNetwork.JoinRandomRoom();
    }

    // ルームに入室すると呼ばれる
    void OnJoinedRoom()
    {
//        PhaseManager.Instance.SetNextPhase("EnemyTurn");
        Debug.Log("ルームへ入室しました。");
    }

    // ルームの入室に失敗すると呼ばれる
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("ルームの入室に失敗しました。");

//        PhaseManager.Instance.SetNextPhase("EnemyTurn");
        PhaseManager.Instance.SetNextPhase("SelectUseCard");
        // ルームがないと入室に失敗するため、その時は自分で作る
        // 引数でルーム名を指定できる
        PhotonNetwork.CreateRoom("roomName", _roomOptions, null);
        Debug.Log("ルームを作成しました。");
    }
}