using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour {

    //生成するマップオブジェクト
    public GameObject[] mapObject;

    //生成するマップの大きさ
    public int maxPosX = 5;
    public int maxPosZ = 7;

    //マップオブジェクトを生成する座標
    private int posX = 0;
    private int posZ = 0;

    // Use this for initialization
    void Start()
    {
        //
        for (; posX < maxPosX; posX++)
        {
            for (; posZ < maxPosZ; posZ++)
            {
                int objectNumber = Random.Range(0, mapObject.Length);

                Vector3 position = new Vector3(posX, 0, posZ);
                Quaternion q = new Quaternion();
                q = Quaternion.identity;

                Instantiate(mapObject[objectNumber], position, q);
            }

            posZ = 0;
        }
    }
}
