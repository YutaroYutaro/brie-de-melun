using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndManager : SingletonMonoBehaviour<GameEndManager>
{
    public void GameEnd()
    {
        Debug.Log("You Win!");
    }
}