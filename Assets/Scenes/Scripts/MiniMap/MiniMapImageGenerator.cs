using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;

public class MiniMapImageGenerator : MonoBehaviour
{
    public GameObject MiniMapImage;

    public GameObject Minimap;

    //生成するマップの大きさ
    public int MaxPosX = 5;
    public int MaxPosZ = 7;

    public List<GameObject> MiniMapImageList;

    void Start()
    {
        for (int posX = 0; posX < MaxPosX; ++posX)
        {
            for (int posZ = 0; posZ < MaxPosZ; ++posZ)
            {
                GameObject miniMapImageInstance = Instantiate(MiniMapImage);
                miniMapImageInstance.GetComponent<MiniMapImageInstancePosition>().PosX = posX;
                miniMapImageInstance.GetComponent<MiniMapImageInstancePosition>().PosZ = posZ;
                miniMapImageInstance.transform.SetParent(Minimap.transform, false);
                MiniMapImageList.Add(miniMapImageInstance);
            }
        }
    }
}