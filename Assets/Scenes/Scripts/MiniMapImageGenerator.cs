using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;

public class MiniMapImageGenerator : MonoBehaviour
{
    public GameObject MiniMapImage = null;

    public GameObject Minimap = null;
//	public int NumberOfMiniMapImage = 35;

    //生成するマップの大きさ
    public int maxPosX = 5;
    public int maxPosZ = 7;

    //マップオブジェクトを生成する座標
    private int posX = 0;
    private int posZ = 0;


    public List<GameObject> MiniMapImageList = null;

    // Use this for initialization
    void Start()
    {
//		for (int i = 0; i < NumberOfMiniMapImage; i++)
//		{
//			GameObject miniMapImageInstance = Instantiate(MiniMapImage);
//			miniMapImageInstance.GetComponent<MiniMapImageInstancePosition>().PosX = ;
//			miniMapImageInstance.transform.SetParent(Minimap.transform, false);
//			MiniMapImageList.Add(miniMapImageInstance);
//			
//			Debug.Log(miniMapImageInstance.transform.position);
//		}

        for (; posX < maxPosX; posX++)
        {
            for (; posZ < maxPosZ; posZ++)
            {
                GameObject miniMapImageInstance = Instantiate(MiniMapImage);
                miniMapImageInstance.GetComponent<MiniMapImageInstancePosition>().PosX = posX;
                miniMapImageInstance.GetComponent<MiniMapImageInstancePosition>().PosZ = posZ;
                miniMapImageInstance.transform.SetParent(Minimap.transform, false);
                MiniMapImageList.Add(miniMapImageInstance);

                //Debug.Log("PosX: " + miniMapImageInstance.GetComponent<MiniMapImageInstancePosition>().PosX + " PosZ: " + miniMapImageInstance.GetComponent<MiniMapImageInstancePosition>().PosZ + " ID: " + miniMapImageInstance.gameObject.GetInstanceID());
            }

            posZ = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}