using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;

public class MiniMapImageGenerator : MonoBehaviour
{
    public GameObject MiniMapImage = null;

    public GameObject Minimap = null;

    //生成するマップの大きさ
    public int maxPosX = 5;
    public int maxPosZ = 7;

    //マップオブジェクトを生成する座標
    private int posX = 0;
    private int posZ = 0;


    public List<GameObject> MiniMapImageList = null;

    void Start()
    {
        for (; posX < maxPosX; posX++)
        {
            for (; posZ < maxPosZ; posZ++)
            {
                GameObject miniMapImageInstance = Instantiate(MiniMapImage);
                miniMapImageInstance.GetComponent<MiniMapImageInstancePosition>().PosX = posX;
                miniMapImageInstance.GetComponent<MiniMapImageInstancePosition>().PosZ = posZ;
                miniMapImageInstance.transform.SetParent(Minimap.transform, false);
                MiniMapImageList.Add(miniMapImageInstance);
            }

            posZ = 0;
        }
    }
}