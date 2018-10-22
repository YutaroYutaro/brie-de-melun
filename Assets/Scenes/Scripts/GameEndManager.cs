using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndManager : SingletonMonoBehaviour<GameEndManager>
{
    public void GameEnd()
    {
        Debug.Log("You Win!");
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
}