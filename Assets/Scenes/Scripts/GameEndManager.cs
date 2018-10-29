using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndManager : SingletonMonoBehaviour<GameEndManager>
{
    [SerializeField] private Text _winText;

    public void GameEnd()
    {
        Debug.Log("You Win!");
        _winText.GetComponent<Text>().enabled = true;
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