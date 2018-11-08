using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapImageGenerator : MonoBehaviour
{
    public GameObject MiniMapImage;

    public GameObject Minimap;

    //生成するマップの大きさ
    public int MaxPosX = 5;
    public int MaxPosZ = 7;

    public List<GameObject> MiniMapImageList;

    [SerializeField] private Sprite _field;

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

                if (posZ == 0 && (posX == 1 || posX == 2 || posX == 3))
                {
                    Image img = miniMapImageInstance.GetComponent<Image>();
                    img.sprite = _field;
                }
            }
        }
    }
}