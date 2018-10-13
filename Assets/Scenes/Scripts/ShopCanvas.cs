using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCanvas : SingletonMonoBehaviour<ShopCanvas>
{
    protected override void Init()
    {
        base.Init();

        /*初期化処理を色々と*/
        GetComponent<Canvas>().enabled = false;
    }

    public void ShopCanvasOpen()
    {
        GetComponent<Canvas>().enabled = true;
    }

    public void ShopCanvasClose()
    {
        GetComponent<Canvas>().enabled = false;
    }
}