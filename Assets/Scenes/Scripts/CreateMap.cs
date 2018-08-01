using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    //生成するマップオブジェクト
    public GameObject[] mapObject;

    //生成するマップの大きさ
    public int maxPosX = 5;
    public int maxPosZ = 7;

    //マップオブジェクトを生成する座標
    private int posX = 0;
    private int posZ = 0;

    //マップ重み表
    private int[,] mapWeight;

    // Use this for initialization
    void Start()
    {
        //マップの座標
        mapWeight = new int[maxPosX, maxPosZ];

        //左下からマップ生成
        for (; posX < maxPosX; posX++)
        {
            for (; posZ < maxPosZ; posZ++)
            {
                //生成するマップオブジェクトを選択
                int objectNumber = Random.Range(0, mapObject.Length);

                //マップオブジェクトごとの重みを保存
                if (mapObject[objectNumber].name == "Field")
                {
                    mapWeight[posX, posZ] = 1;
                }
                else if (mapObject[objectNumber].name == "Forest")
                {
                    mapWeight[posX, posZ] = 2;
                }
                else if (mapObject[objectNumber].name == "GoldMine")
                {
                    mapWeight[posX, posZ] = 1;
                }
                else if (mapObject[objectNumber].name == "Mount")
                {
                    mapWeight[posX, posZ] = 5;
                }

                //オブジェクトの設置位置を設定
                Vector3 position = new Vector3(posX, 0, posZ);
                Quaternion q = new Quaternion();
                q = Quaternion.identity;

                //オブジェクトの生成
                Instantiate(mapObject[objectNumber], position, q);
            }

            posZ = 0;
        }

        posX = 0;

//        for (; posX < maxPosX; posX++)
//        {
//            for (; posZ < maxPosZ; posZ++)
//            {
//                Debug.Log("(posX, posZ) = (" + posX + ", " + posZ + ") :" + mapWeight[posX, posZ]);
//            }
//
//            posZ = 0;
//        }
    }

    //外部からマップの重み表を取得するメソッド
    public int[,] GetMapWeight()
    {
        return mapWeight;
    }
}